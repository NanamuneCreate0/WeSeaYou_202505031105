using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EVExecuter : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    public string CommandType => "EV";
    public bool IsAutoAdvance => false;

    [SerializeField] MoveEvent _move;
    [SerializeField] StandEvent _stand;
    [SerializeField] WaitEvent _wait;
    [SerializeField] private MonoBehaviour[] _specialEvents;
    [SerializeField] private Transform[] _charas;

    private readonly Dictionary<string, Transform> _charaByName = new Dictionary<string, Transform>();
    private readonly Dictionary<string, ISpecialEvent> _specialByID = new Dictionary<string, ISpecialEvent>();

    private Action _pendingComplete; // 待機中だけ値が入る
    private Action _skipEV;          // 待機中だけ値が入る（今はクリックスキップ無効）
    private int _waitId;             // 古いコールバックを無視するための番号

    private const string WALK = "WALK";
    private const string DUSH = "DUSH";
    private const string STAND = "STAND";
    private const string WAIT = "WAIT";
    private const int NAME_INDEX = 2;

    // IDの中から探すコマンド名
    private static readonly HashSet<string> COMMANDS = new HashSet<string> { WALK, DUSH, STAND, WAIT };

    void Awake()
    {
        foreach (var chara in _charas)
            _charaByName[chara.name] = chara;

        foreach (var component in _specialEvents)
        {
            if (component is ISpecialEvent ev)
                _specialByID[ev.TargetID] = ev;
            else
                Debug.LogWarning($"{component.name} は ISpecialEvent を実装していません");
        }
    }

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        ClearWait(); // 前回の残りを消す

        string[] idParam = data.ID.Split('_');
        bool wait = ShouldWaitForEnd(data);

        // ★ 例外処理：専用イベントのIDなら、形式やキャラの有無に関係なく最優先
        if (_specialByID.TryGetValue(data.ID, out var special))
        {
            Transform specialChara = FindChara(idParam); // 見つからなければ null
            Run(cb => special.Execute(data, specialChara, cb), special.Skip, wait, onComplete);
            return;
        }

        // ここから下は通常のEV（キャラ必須）
        Transform chara = FindChara(idParam);
        if (chara == null)
        {
            Debug.LogWarning($"Chara not found: {data.ID}");
            onComplete?.Invoke();
            return;
        }

        Animator animator = chara.GetComponent<Animator>();
        if (animator != null) animator.Play(data.ID);

        string command = FindCommand(idParam);

        switch (command)
        {
            case WALK:
            case DUSH:
                if (!float.TryParse(data.Param, NumberStyles.Float, CultureInfo.InvariantCulture, out float pos))
                {
                    Debug.LogWarning($"座標が読めない: {data.Param}");
                    onComplete?.Invoke();
                    return;
                }
                bool isWalk = command == WALK;
                Run(cb =>
                {
                    if (isWalk) _move.WalkTo(pos, animator, chara, cb);
                    else _move.DushTo(pos, animator, chara, cb);
                }, _move.ExecuteImmediate, wait, onComplete);
                break;

            case STAND:
                Run(cb => _stand.Execute(data, context, cb), null, wait, onComplete);
                break;

            case WAIT:
                onComplete?.Invoke();
                break;

            default: // コマンド名が見つからなかった（null）
                Debug.LogWarning($"Unknown EV command: {data.ID}");
                onComplete?.Invoke();
                break;
        }
    }

    // IDの NAME_INDEX の位置からキャラを探す（なければ null）
    private Transform FindChara(string[] idParam)
    {
        if (idParam.Length <= NAME_INDEX) return null;
        _charaByName.TryGetValue(idParam[NAME_INDEX], out Transform chara);
        return chara;
    }

    // 区切ったIDの中から、最初に見つかったコマンド名を返す（なければnull）
    private static string FindCommand(string[] idParam)
    {
        foreach (var part in idParam)
            if (COMMANDS.Contains(part))
                return part;
        return null;
    }

    private bool ShouldWaitForEnd(ScenarioLine data)
    {
        return data.EventWaitFlag == 1;
    }

    // 待つ・待たないの分岐をまとめたもの
    private void Run(Action<Action> start, Action skip, bool wait, Action onComplete)
    {
        if (wait)
        {
            _pendingComplete = onComplete;
            _skipEV = skip;
            int id = ++_waitId;
            start(() => FinishIfCurrent(id));
        }
        else
        {
            start(null);            // イベントは裏で動かしたまま
            onComplete?.Invoke();   // すぐ次の行へ
        }
    }

    public void HandleAdvanceInput()
    {
        // 待機中のクリックでは進めない（イベントの終了でのみ進む）
        // スキップを復活させたいときは、以下のコメントを外す
        // if (_pendingComplete == null || _skipEV == null) return;
        // _skipEV();
        // Finish();
    }

    private void FinishIfCurrent(int id)
    {
        if (id != _waitId) return;
        Finish();
    }

    private void Finish()
    {
        var cb = _pendingComplete;
        ClearWait();
        cb?.Invoke();
    }

    private void ClearWait()
    {
        _pendingComplete = null;
        _skipEV = null;
    }
}