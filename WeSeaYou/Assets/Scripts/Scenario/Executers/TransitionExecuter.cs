using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionExecuter : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "TRANSITION";
    public bool IsAutoAdvance => true;

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        if(data.ID == "TestScene")
        {
            Debug.Log("TestSceneに遷移します");
            return;
        }
        SceneManager.LoadScene(data.ID);  // シーン名で指定
        onComplete?.Invoke();
    }
}
