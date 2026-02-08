using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using NUnit.Framework;

public class ScenarioDisplayer : MonoBehaviour
{
    /*[System.Serializable]
    public class Step
    {
        public int triggerCount;       // 表示させるカウント数

        // 表示する対象
        public GameObject StillObject;
        public GameObject FaceObject;
        public GameObject NowFaceObject;
    }*/

    //public Step[] steps;
    
    [SerializeField, TextArea(1, 3)] private string[] messages;             // 表示したい文章リスト
    [SerializeField] private TMP_Text messageText;                          // 表示用Text
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private string StageName;
    [SerializeField] private float charDelay = 0.05f;                       // 文字送りの速さ

    private Coroutine _typingCoroutine;
    private int messageIndex;

    public bool IsTyping { get; private set; } = false;

    void Start()
    {
        //StartCoroutine(StartScenario());
    }

    IEnumerator StartScenario()
    {
        //StartCoroutine(TypeMessage(messages[messageIndex]));
        yield break;
    }

    public void PlayLine(ScenarioLine line)
    {
        // もし動いていたら一旦止める（連打対策）
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypeMessage(line));
    }

    // ★追加：一瞬で全表示にするメソッド
    public void Skip()
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);

        // 文字を最大まで表示
        messageText.maxVisibleCharacters = messageText.textInfo.characterCount;
        IsTyping = false;
    }

    private IEnumerator TypeMessage(ScenarioLine line)
    {
        IsTyping = true;
        messageText.maxVisibleCharacters = 0;

        NameText.text = line.Name;
        messageText.text = line.Message;

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
