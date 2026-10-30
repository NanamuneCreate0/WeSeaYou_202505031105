using UnityEngine;

public class ScenarioTrriger : MonoBehaviour
{
    /*[SerializeField] private ScenarioLoader _scenarioLoader;
    [SerializeField] private ScenarioSystem _scenarioSystem;
    [SerializeField] private TextExecutor _textExecutor;*/

    [Header("Scenario Data")]
    [SerializeField] private TextAsset _scenarioData;
    [SerializeField] private TextDataSO _textData;
    [SerializeField] private Vector2[] _firstCharacterPosition;
    [SerializeField] private GameObject[] _Charas;

    const int CHARACTER_INDEX_SEA = 0;
    const int CHARACTER_INDEX_MARE = 1;
    public void StartScenario(ScenarioLoader _scenarioLoader, TextExecutor _textExecutor, ScenarioSystem _scenarioSystem)
    {
        Debug.Log("Triggering scenario...");
        _Charas[CHARACTER_INDEX_SEA].transform.position = _firstCharacterPosition[CHARACTER_INDEX_SEA];
        _Charas[CHARACTER_INDEX_MARE].transform.position = _firstCharacterPosition[CHARACTER_INDEX_MARE];
        _scenarioLoader.LoadScenario(_scenarioData);
        _textExecutor.SetTable(new TextTable(_textData));
        _scenarioSystem.StartScenario();
    }
}

