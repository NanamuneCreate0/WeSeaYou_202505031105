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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_actionKey))
        {
            Debug.Log("Triggering scenario...");
            _scenarioLoader.LoadScenario(_scenarioData);
            _textExecutor.SetTable(new TextTable(_textData));
            _scenarioSystem.StartScenario();
        }
    }
}
