using UnityEngine;
using UnityEngine.UI;

public class StillExecuter : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    public string CommandType => "STILL";
    public bool IsAutoAdvance => true;

    [SerializeField] private Image _still;
    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    public void Execute(ScenarioLine data, IScenarioContext context, System.Action onComplete)
    {
        if (data.ID == "OFF")
        {
            _still.sprite = null;
            _still.enabled = false;
        }
        else
        {
            _still.sprite = Resources.Load<Sprite>("Sprites/Stills/" + data.ID);
            _still.enabled = true;
        }
        onComplete?.Invoke();
    }

    public void HandleAdvanceInput()
    {
        Debug.Log("Advance input received for STILLExecuter");
    }
}
