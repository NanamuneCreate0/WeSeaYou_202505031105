using UnityEngine;

public class ScenarioStarter : MonoBehaviour
{
    [Header("Scenario Trigger")]
    [SerializeField] private ScenarioTrriger[] _scenarioTrigger;
    [Header("Scenario Data")]
    [SerializeField] private ScenarioLoader _scenarioLoader;
    [SerializeField] private TextExecutor _textExecutor;
    [SerializeField] private ScenarioSystem _scenarioSystem;

    private int _currentTriggerIndex = 0;


    void Start()
    {
        Debug.Log("シナリオを開始します");
        _currentTriggerIndex = 1 + (int)PublicStaticStatus.ClearedChapter;
        _currentTriggerIndex--;
        _scenarioTrigger[_currentTriggerIndex].StartScenario(_scenarioLoader, _textExecutor, _scenarioSystem);
    }
}
