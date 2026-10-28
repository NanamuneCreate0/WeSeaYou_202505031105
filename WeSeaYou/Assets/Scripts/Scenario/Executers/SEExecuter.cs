using System;
using System.Collections;
using UnityEngine;

public class SEExecutor : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    [SerializeField] private AudioSource _SESource;
    public string CommandType => "SE";
    public bool IsAutoAdvance => true;

    private Action _pendingComplete;   // 待機中だけ値が入る
    private Coroutine _waitRoutine;

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        if (_SESource == null)
        {
            Debug.LogWarning("SEExecutor: AudioSourceが未設定");
            onComplete?.Invoke();   // ここで止まるとシナリオが進まなくなるので必ず呼ぶ
            return;
        }

        AudioClip clip = Resources.Load<AudioClip>(data.ID);
        if (clip == null)
        {
            Debug.LogWarning($"SEExecutor: クリップが見つからない: {data.ID}");
            onComplete?.Invoke();
            return;
        }

        _SESource.clip = clip;
        _SESource.Play();

        if (!ShouldWaitForEnd(data))
        {
            onComplete?.Invoke();   // 従来どおり鳴らしてすぐ次へ
            return;
        }

        // 待機モード：終わるか、入力でスキップされるまで onComplete を保留
        _pendingComplete = onComplete;
        _waitRoutine = StartCoroutine(WaitForSEEnd());
    }

    // ★「ある条件」をここに書く（ScenarioLineの実際の列に合わせて変更してください）
    private bool ShouldWaitForEnd(ScenarioLine data)
    {
        Debug.Log($"ShouldWaitForEnd: {data.EventWaitFlag}");
        return data.EventWaitFlag == 1;
    }

    private IEnumerator WaitForSEEnd()
    {
        yield return new WaitWhile(() => _SESource.isPlaying);
        _waitRoutine = null;
        Finish();
    }

    // 待機中に入力されたらSEを止めて次へ進める
    public void HandleAdvanceInput()
    {
        if (_pendingComplete == null) return; // 待機していないときは無視

        if (_waitRoutine != null)
        {
            StopCoroutine(_waitRoutine);
            _waitRoutine = null;
        }
        _SESource.Stop();
        Finish();
    }

    private void Finish()
    {
        // 二重に呼ばれないよう、先にnullにしてから実行
        var callback = _pendingComplete;
        _pendingComplete = null;
        callback?.Invoke();
    }

    private void OnDisable()
    {
        if (_waitRoutine != null) StopCoroutine(_waitRoutine);
        _waitRoutine = null;
        _pendingComplete = null;
    }
}