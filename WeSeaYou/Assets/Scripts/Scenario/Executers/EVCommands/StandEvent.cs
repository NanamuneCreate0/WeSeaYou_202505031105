using UnityEngine;

public class StandEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(ScenarioLine data, IScenarioContext context, System.Action onComplete)
    {
        // Standコマンドの処理を実装する
        Debug.Log($"Stand Command Executed with data: {data}");
        // ここでキャラクターの立ち止まり処理を実装する
        onComplete?.Invoke();
    }
}
