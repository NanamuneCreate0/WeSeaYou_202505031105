using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraExecuter : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "CAMERA";
    public bool IsAutoAdvance => true;

    [SerializeField] private Transform[] _charas;
    [SerializeField] private FollowEvent _followEvent;

    private readonly Dictionary<string, Transform> _charaByName = new Dictionary<string, Transform>();
    private Action _reset;

    // IDの形式:
    //   CARA_FOLLOW_(キャラ名) → [0] CARA [1] FOLLOW [2] キャラ名 → 中心に寄せて追従
    //   PAN_RIGHT_(キャラ名)   → [0] PAN  [1] RIGHT  [2] キャラ名 → 今の距離を保って追従
    private const string FOLLOW = "FOLLOW";
    private const string PAN = "PAN";
    private const int NAME_INDEX = 2;

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

        if (param == 0)
        {
            ResetCamera();
        }
        else if (param == 1)
        {
            string[] idParam = data.ID.Split('_');
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

        _cameraType = CameraCommandType.FollowTo;
        _reset = _followEvent.ResetFollowSetting;
        _followEvent.StartFollowSetting(target, keepOffset);
    }
}