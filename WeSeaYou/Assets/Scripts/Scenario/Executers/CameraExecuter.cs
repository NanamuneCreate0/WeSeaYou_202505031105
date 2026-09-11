using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraExecuter : MonoBehaviour, IScenarioCommand

{
    public string CommandType => "CAMERA";
    public bool IsAutoAdvance => true;

    [SerializeField] private Transform[] _charas;
    [SerializeField] private FollowEvent _followEvent;
    private string[] _idParam;
    private Action _reset;
    private Dictionary<Transform, string> _charaID = new Dictionary<Transform, string>();

    private const string FOLLOW = "FOLLOW";
    private const int EVENT_INDEX = 0;
    private const int NAME_INDEX = 1;
    enum CameraCommandType
    {
        None,
        MoveTo,
        RotateTo,
        ZoomTo,
        FollowTo

    };
    CameraCommandType _cameraType = CameraCommandType.None;
    private void Awake()
    {
        foreach (var chara in _charas)
        {
            _charaID.Add(chara, chara.name);
            Debug.Log($"Chara ID: {chara.name}");
        }
    }
    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        int param = int.Parse(data.Param);
        if (param == 0)
        {
            ResetCamera();
        }
        else if (param == 1)
        {
            _idParam = data.ID.Split('_');
            switch (_idParam[EVENT_INDEX])
            {
                case FOLLOW:
                    FollowMode();
                    break;
            }
        }
        onComplete?.Invoke();
    }

    private void ResetCamera()
    {
        _cameraType = CameraCommandType.None;
        _reset?.Invoke();
        _reset = null;
        Debug.Log("Executing NONE command in CameraExecuter");
    }

    private void FollowMode()
    {
        Transform target = null;
        foreach (var chara in _charas)
        {
            if (_charaID.TryGetValue(chara, out string id) && id == _idParam[NAME_INDEX])
            {
                target = chara;
                break;
            }
        }
        _cameraType = CameraCommandType.FollowTo;
        _reset = _followEvent.ResetFollowSetting;
        _followEvent.StartFollowSetting(target);
        Debug.Log($"Executing FOLLOW command for character: {_idParam[NAME_INDEX]}");
    }
}
