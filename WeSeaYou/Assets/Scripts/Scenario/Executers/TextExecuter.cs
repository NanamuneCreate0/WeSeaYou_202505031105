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
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private float charDelay = 0.05f;

    private TextTable _table;
    private Coroutine _typingCoroutine;
    private Action _onComplete;
    private bool IsTyping { get; set; } = false;

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
        // ID引き。見つからなければ警告して即完了(シナリオを詰まらせない)
        if (!_table.TryGet(data.ID, out var entry))
        {
            Debug.LogWarning($"テキストID未登録: {data.ID}");
            onComplete?.Invoke();
            return;
        }

        _onComplete = onComplete;   // ポケットにしまう
        PlayLine(entry);
    }

    private void PlayLine(TextEntry entry)
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypeMessage(entry));
    }

    public void HandleAdvanceInput()
    {
        if (IsTyping)
        {
            // 1回目のクリック: 全文表示にするだけ
            messageText.maxVisibleCharacters = messageText.textInfo.characterCount;
            IsTyping = false;
            return;
        }

        // 2回目のクリック: TEXTコマンドの完了時刻
        if (_onComplete != null)
        {
            var cb = _onComplete;
            _onComplete = null;   // 先にnull(二重発火ガード)
            cb();
        }
    }

    private IEnumerator TypeMessage(TextEntry entry)
    {
        IsTyping = true;
        messageText.maxVisibleCharacters = 0;

        NameText.text = SetName(entry);        // ← 出どころがTextEntryに
        messageText.text = entry.JPText;   // ←

        messageText.ForceMeshUpdate();
        int totalCharacters = messageText.textInfo.characterCount;

        for (int i = 0; i <= totalCharacters; i++)
        {
            if (!IsTyping) break;
            messageText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(charDelay);
        }

        IsTyping = false;
    }

    private string SetName(TextEntry entry)
    {
        switch(entry.Name)
        {
            case "MARE":
                return "マーレ";
            case "SEA":
                return "シー";
            case "NULL":
                return "";
            default:
                return entry.Name;
        }
    }
}