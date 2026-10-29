using System;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using UnityEngine;

public class CameraExecuter : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "CAMERA";
    public bool IsAutoAdvance => true;

    [SerializeField] private Transform[] _charas;
    [SerializeField] private FollowEvent _followEvent;

    [Header("MoveTo")]
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveDuration = 1f;
    [SerializeField] private Ease _moveEase = Ease.InOutSine;

    private readonly Dictionary<string, Transform> _charaByName = new Dictionary<string, Transform>();
    private Action _reset;

    // Param:
    //   0 → リセット
    //   1 → 追従
    //   2 → 移動
    //
    // IDの形式:
    //   CARA_FOLLOW_(キャラ名) → [0] CARA [1] FOLLOW [2] キャラ名 → 中心に寄せて追従
    //   PAN_RIGHT_(キャラ名)   → [0] PAN  [1] RIGHT  [2] キャラ名 → 今の距離を保って追従
    //   MOVE_X_(x座標)         → [0] MOVE [1] X      [2] x座標   → 指定したxまで移動
    private const string FOLLOW = "FOLLOW";
    private const string PAN = "PAN";
    private const string MOVE = "MOVE";
    private const string AXIS_X = "X";
    private const int NAME_INDEX = 2;
    private const int VALUE_INDEX = 2;

    private const int PARAM_RESET = 0;
    private const int PARAM_FOLLOW = 1;
    private const int PARAM_MOVE = 2;

    enum CameraCommandType { None, MoveTo, RotateTo, ZoomTo, FollowTo }
    private CameraCommandType _cameraType = CameraCommandType.None;

    private void Awake()
    {
        foreach (var chara in _charas)
            _charaByName[chara.name] = chara;
    }

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        if (!int.TryParse(data.Param, out int param))
        {
            Debug.LogWarning($"CAMERAのParamが読めない: {data.Param}");
            onComplete?.Invoke();
            return;
        }

        string[] idParam = data.ID.Split('_');

        switch (param)
        {
            case PARAM_RESET:
                ResetCamera();
                break;

            case PARAM_FOLLOW:
                if (idParam.Length <= NAME_INDEX)
                {
                    Debug.LogWarning($"CAMERAのIDの形式が不正: {data.ID}");
                }
                else if (idParam[0] == PAN)
                {
                    FollowMode(idParam[NAME_INDEX], keepOffset: true);   // 距離を保って追従
                }
                else if (idParam[1] == FOLLOW)
                {
                    FollowMode(idParam[NAME_INDEX], keepOffset: false);  // 中心に寄せて追従
                }
                else
                {
                    Debug.LogWarning($"Unknown camera command: {data.ID}");
                }
                break;

            case PARAM_MOVE:
                // 移動は着いてから完了するので、ここで return
                MoveMode(idParam, data.ID, onComplete);
                return;

            default:
                Debug.LogWarning($"CAMERAのParamが未対応: {param}");
                break;
        }

        onComplete?.Invoke();
    }

    private void ResetCamera()
    {
        _cameraType = CameraCommandType.None;
        _reset?.Invoke();
        _reset = null;
    }

    private void FollowMode(string charaName, bool keepOffset)
    {
        if (!_charaByName.TryGetValue(charaName, out Transform target))
        {
            Debug.LogWarning($"Chara not found: {charaName}");
            return;
        }
        Debug.Log($"FollowMode: [{charaName}] keepOffset={keepOffset}");
        _cameraType = CameraCommandType.FollowTo;
        _reset = _followEvent.ResetFollowSetting;
        _followEvent.StartFollowSetting(target, keepOffset);
    }

    private void MoveMode(string[] idParam, string rawID, Action onComplete)
    {
        if (idParam.Length <= VALUE_INDEX || idParam[0] != MOVE || idParam[1] != AXIS_X)
        {
            Debug.LogWarning($"CAMERAのIDの形式が不正: {rawID}");
            onComplete?.Invoke();
            return;
        }

        if (!float.TryParse(idParam[VALUE_INDEX], NumberStyles.Float, CultureInfo.InvariantCulture, out float targetX))
        {
            Debug.LogWarning($"CAMERAの座標が読めない: {rawID}");
            onComplete?.Invoke();
            return;
        }

        // 追従中なら先に解除（追従が残っているとカメラを引き戻される）
        if (_cameraType == CameraCommandType.FollowTo) ResetCamera();

        _cameraType = CameraCommandType.MoveTo;
        _reset = () => _camera.DOKill();

        _camera.DOKill();
        _camera.DOMoveX(targetX, _moveDuration)
            .SetEase(_moveEase)
            .SetLink(gameObject)
            .OnComplete(() => onComplete?.Invoke());
    }
}