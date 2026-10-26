using UnityEngine;

public class ScenarioTrriger : MonoBehaviour
{
    /*[SerializeField] private ScenarioLoader _scenarioLoader;
    [SerializeField] private ScenarioSystem _scenarioSystem;
    [SerializeField] private TextExecutor _textExecutor;*/

    [Header("Scenario Data")]
    [SerializeField] private TextAsset _scenarioData;
    [SerializeField] private TextDataSO _textData;

    public void StartScenario(ScenarioLoader _scenarioLoader, TextExecutor _textExecutor, ScenarioSystem _scenarioSystem)
    {
        Debug.Log("Triggering scenario...");
        _scenarioLoader.LoadScenario(_scenarioData);
        _textExecutor.SetTable(new TextTable(_textData));
        _scenarioSystem.StartScenario();
    }
}

