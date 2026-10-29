using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextExecutor : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    public string CommandType => "TEXT";
    public bool IsAutoAdvance => false;

    [Header("テキストデータ")]
    [SerializeField] private TextDataSO _scenarioText;   // ← 缶詰をInspectorでD&D

    [Header("表示先UI")]
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private GameObject[] _namePanels;

    [Header("表示速度")]
    [SerializeField] private float charDelay = 0.05f;
    [SerializeField] private float slowCharDelay = 0.15f; // param=2 のときの1文字あたりの待ち時間

    private TextTable _table;
    private Coroutine _typingCoroutine;
    private Action _onComplete;

    private const int NAME_INDEX_SEA = 0;
    private const int NAME_INDEX_MARE = 1;
    private const int NAME_INDEX_FKSEA = 2;
    private const int NAME_INDEX_FKMARE = 3;

    private const string PARAM_SLOW = "Lv2";

    private bool IsTyping { get; set; } = false;

    public event Action OnTextAdvanced;

    private void Awake()
    {
        //_table = new TextTable(_scenarioText);
    }

    public void SetTable(TextTable table)
    {
        _table = table;
    }

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        for (int i = 0; i < _namePanels.Length; i++)
        {
            _namePanels[i].SetActive(false);
        }

        // ID引き。見つからなければ警告して即完了(シナリオを詰まらせない)
        if (!_table.TryGet(data.ID, out var entry))
        {
            Debug.LogWarning($"テキストID未登録: {data.ID}");
            onComplete?.Invoke();
            return;
        }

        _onComplete = onComplete;   // ポケットにしまう

        float delay = data.Param == PARAM_SLOW ? slowCharDelay : charDelay;
        PlayLine(entry, delay);
    }

    private void PlayLine(TextEntry entry, float delay)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        if (string.IsNullOrEmpty(entry.JPText))
        {
            _messageText.text = "";
            _nameText.text = "";
            _onComplete?.Invoke();
            return;
        }
        _typingCoroutine = StartCoroutine(TypeMessage(entry, delay));
    }

    public void HandleAdvanceInput()
    {
        if (IsTyping)
        {
            // 1回目のクリック: 全文表示にするだけ
            _messageText.maxVisibleCharacters = _messageText.textInfo.characterCount;
            IsTyping = false;
            return;
        }

        // 2回目のクリック: TEXTコマンドの完了時刻
        if (_onComplete != null)
        {
            var cb = _onComplete;
            _onComplete = null;   // 先にnull(二重発火ガード)
            OnTextAdvanced?.Invoke();
            cb();
        }
    }

    private IEnumerator TypeMessage(TextEntry entry, float delay)
    {
        IsTyping = true;
        _messageText.maxVisibleCharacters = 0;

        SetName(entry);
        _messageText.text = entry.JPText;

        _messageText.ForceMeshUpdate();
        int totalCharacters = _messageText.textInfo.characterCount;

        for (int i = 0; i <= totalCharacters; i++)
        {
            if (!IsTyping) break;
            _messageText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(delay);
        }

        IsTyping = false;
    }

    private void SetName(TextEntry entry)
    {
        switch (entry.Name)
        {
            case "MARE":
                _namePanels[NAME_INDEX_MARE].SetActive(true);
                break;
            case "SEA":
                _namePanels[NAME_INDEX_SEA].SetActive(true);
                break;
            case "FKSEA":
                _namePanels[NAME_INDEX_FKSEA].SetActive(true);
                break;
            case "FKMARE":
                _namePanels[NAME_INDEX_FKMARE].SetActive(true);
                break;
            default:
                Debug.LogWarning($"Unknown name: {entry.Name}");
                break;
        }
    }
}