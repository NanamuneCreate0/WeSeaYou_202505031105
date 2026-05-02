using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class TextExecutor : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    public string CommandType => "TEXT";
    public bool IsAutoAdvance => false;
    [SerializeField] private List<CharacterReference> _characterDirectory;
    [SerializeField] private TMP_Text messageText;                          // 表示用Text
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private float charDelay = 0.05f;

    private Coroutine _typingCoroutine;
    private bool IsTyping { get; set; } = false;

    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        PlayLine(data);
    }
    private void PlayLine(ScenarioLine line)
    {
        // もし動いていたら一旦止める（連打対策）
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypeMessage(line));
    }
    public bool HandleAdvanceInput()
    {
        if (IsTyping)
        {
            // タイピング中なら即座に全文表示
            messageText.maxVisibleCharacters = messageText.textInfo.characterCount;
            IsTyping = false;
            return false; // まだ次の行には進まない
        }
        else
        {
            return true; // 次の行に進んでいい
        }
    }
    private IEnumerator TypeMessage(ScenarioLine line)
    {
        IsTyping = true;
        messageText.maxVisibleCharacters = 0;

        NameText.text = line.Name;
        messageText.text = line.JPText;

        messageText.ForceMeshUpdate();
        int totalCharacters = messageText.textInfo.characterCount;

        for (int i = 0; i <= totalCharacters; i++)
        {
            messageText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(charDelay);
        }

        IsTyping = false;
    }
}
