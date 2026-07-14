using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScenarioSystem : MonoBehaviour, IScenarioContext
{
    public void JumpTo(int index)        => _currentIndex = index;
    public void EnterWait()              => _state = ScenarioState.Wait;
    public void ResumeScenario()         => _state = ScenarioState.OnScenario;
    public void ExitScenario()           => _state = ScenarioState.NoScenario;
    // Inspector から各コマンドを設定
    [SerializeField] private MonoBehaviour[] _commandComponents;
    [SerializeField] private CinemaScopeManager _cinema;

    // 実行時に使う辞書
    private Dictionary<string, IScenarioCommand> _commands;

    private List<ScenarioLine> _csvData;
    private int _currentIndex;
    public enum ScenarioState { NoScenario, OnScenario, Wait }
    private ScenarioState _state = ScenarioState.NoScenario;

    private void Awake()
    {
        // コマンドを辞書に登録
        _commands = new Dictionary<string, IScenarioCommand>();

        foreach (var component in _commandComponents)
        {
            if (component is IScenarioCommand command)
            {
                _commands[command.CommandType] = command;
                Debug.Log($"コマンド登録: {command.CommandType}");
            }
        }
    }

    public void SetupData(List<ScenarioLine> data)
    {
        _currentIndex = 0; // 最初は0にして、最初のAdvanceで0から始まるように

        _csvData = data;
    }

    void OnEnable()
    {
        // Instanceが存在するか、actionsがセットされているかを確認
        if (InputManager.Instance != null && InputManager.Instance.actions != null)
        {
            InputManager.Instance.actions.UI.AdvanceText.performed += OnScenario;
        }
        else
        {
            Debug.LogWarning("InputManagerまたはInput Actionsが準備できていません。");
        }
    }

    void OnDisable()
    {
        InputManager.Instance.actions.UI.AdvanceText.performed -= OnScenario;
    }

    private void OnScenario(InputAction.CallbackContext context)
    {
        if (_state != ScenarioState.OnScenario) return;
        if (_commands is IInputReceiver receiver && receiver.HandleAdvanceInput())　return;     // 実行中コマンドが消費したので進めない 
        _currentIndex++;
        ProcessCurrentCommand();// 誰も消費しなかったら次行へ
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _state == ScenarioState.NoScenario)
        {
            StartScenario();
        }
    }

    public void StartScenario()
    {
        StartCoroutine(StartScenarioCorutine());
    }

    private IEnumerator StartScenarioCorutine()
    {
        _state = ScenarioState.OnScenario;

        yield return StartCoroutine(_cinema.PlayCinemaScopeCoroutine());

        yield return new WaitForSeconds(1.5f);

        ProcessCurrentCommand();
        yield return null;
    }



    private void ProcessCurrentCommand()
    {
        if (_currentIndex >= _csvData.Count) return;

        var data = _csvData[_currentIndex];

        // 辞書からコマンドを取得して実行
        if (_commands.TryGetValue(data.Category, out var command))
        {
            command.Execute(data, this, OnCommandContinue);
        }
        else
        {
            Debug.LogWarning($"未知のコマンド: {data.Category}");
            _currentIndex++;
        }
    }

    private void OnCommandContinue()
    {
        _currentIndex++;
        // 必要なら次のコマンドを自動実行
        ProcessCurrentCommand();
    }
}
