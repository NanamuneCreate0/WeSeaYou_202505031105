using UnityEngine;

public class WaitEvent : MonoBehaviour 
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
        Debug.Log($"Wait Command Executed with data: {data}");
    }
}
