using System;
using System.Collections.Generic;
using UnityEngine;

public class EVExecuter : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string CommandType => "EV";
    public bool IsAutoAdvance => false;

    [SerializeField] MoveEvent _move;
    [SerializeField] StandEvent _stand;
    [SerializeField] WaitEvent _wait;
    [SerializeField] private Transform[] _charas;
    private Action _onComplete;
    private Action _skipEV;
    private string[] _idParam;
    private Dictionary<Transform, string> _charaID = new Dictionary<Transform, string>();

    private const string WALK = "W";
    private const string DUSH = "DUSH";
    private const string STAND = "STAND";
    private const string WAIT = "WAIT";
    private const int NAME_INDEX = 2;
    private const int COMMAND_INDEX = 3;



    void Start()
    {
        foreach (var chara in _charas)
        {
            _charaID.Add(chara, chara.name);
            Debug.Log($"Chara ID: {chara.name}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        _idParam = data.ID.Split('_');
        _onComplete = onComplete;

        Transform targetChara = null;

        foreach (var chara in _charas)
        {
            if(_charaID.TryGetValue(chara, out string id) && id == _idParam[NAME_INDEX])
            {
                targetChara = chara;
                break;
            }
        }

        float pos = float.Parse(data.Param);
        switch (_idParam[COMMAND_INDEX])
        {
            case WALK:
                _skipEV = _move.ExecuteImmediate;
                _move.WalkTo(pos, false, targetChara, OnArrived);
                break;
            case DUSH:
                _skipEV = _move.ExecuteImmediate;
                _move.WalkTo(pos, true, targetChara, OnArrived);
                break;
            case WAIT:
                _wait.Execute(data, context, onComplete);
                break;
            case STAND:
                _stand.Execute(data, context, onComplete);
                break;
            default:
                Debug.LogWarning($"Unknown EV command: {data.ID}");
                onComplete?.Invoke();
                break;
        }
    }

    /*private void IDParamHolder()
    {
        
    }*/

    private void OnArrived()
    {
        var cb = _onComplete;
        _onComplete = null;
        cb?.Invoke();
    }

    public void HandleAdvanceInput()
    {
        if(_skipEV != null)
        {
            _skipEV();
        }
    }


}
