using UnityEngine;

public class ScenarioTrigger_Test : MonoBehaviour
{
    [SerializeField] private ScenarioLoader _scenarioLoader;
    [SerializeField] private ScenarioSystem _scenarioSystem;
    [SerializeField] private TextExecutor _textExecutor;
    [SerializeField] private KeyCode _actionKey;

    [Header("Scenario Data")]
    [SerializeField] private TextAsset _scenarioData;
    [SerializeField] private TextDataSO _textData;
    [SerializeField] private Vector2[] _firstCharacterPosition;
    [SerializeField] private GameObject[] _Charas;

    const int CHARACTER_INDEX_SEA = 0;
    const int CHARACTER_INDEX_MARE = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_actionKey))
        {
            Debug.Log("Triggering scenario...");
            _Charas[CHARACTER_INDEX_SEA].transform.position = _firstCharacterPosition[CHARACTER_INDEX_SEA];
            _Charas[CHARACTER_INDEX_MARE].transform.position = _firstCharacterPosition[CHARACTER_INDEX_MARE];
            _scenarioLoader.LoadScenario(_scenarioData);
            _textExecutor.SetTable(new TextTable(_textData));
            _scenarioSystem.StartScenario();
        }
    }
}
