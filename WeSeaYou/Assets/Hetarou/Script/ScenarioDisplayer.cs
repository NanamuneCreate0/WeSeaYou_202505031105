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

    private bool isTyping = false;
    private int messageIndex;


    void Start()
    {
        StartCoroutine(StartScenario());
    }

    IEnumerator StartScenario()
    {
        //StartCoroutine(TypeMessage(messages[messageIndex]));
        yield break;
    }

    public IEnumerator TypeMessage(ScenarioLine line)
    {
        isTyping = true;
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

        isTyping = false;
    }

}
