using UnityEngine;

public class StandEvent : MonoBehaviour
{
    public bool IsDebugLog = false;
    public void Execute(ScenarioLine data, IScenarioContext context, System.Action onComplete)
    {
        // Standコマンドの処理を実装する
        if (IsDebugLog)
        {
            Debug.Log($"Stand Command Executed with data: {data}");
        }
        // ここでキャラクターの立ち止まり処理を実装する

        onComplete?.Invoke();
    }
}
