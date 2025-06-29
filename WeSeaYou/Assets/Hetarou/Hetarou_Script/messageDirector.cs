using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using NUnit.Framework;

public class UItext : MonoBehaviour
{
    [System.Serializable]
    public class Step
    {
        public int triggerCount;       // 表示させるカウント数

        // 表示する対象
        public GameObject StillObject;
        public GameObject FaceObject;
        public GameObject NowFaceObject;
    }

    public Step[] steps;
    public TMP_Text messageText;            // 表示用Text
    [SerializeField, TextArea(1, 3)]
    public string[] messages;           // 表示したい文章リスト
    public float charDelay = 0.05f;     // 文字送りの速さ

    public int messageIndex;
    private bool isTyping = false;

    [SerializeField]
    string StageName;
    void Start()
    {
        StartCoroutine(TypeMessage(messages[messageIndex]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isTyping)
        {
            messageIndex++;

            foreach (Step step in steps)
            {
                if (messageIndex == step.triggerCount)
                {
                    if(step.FaceObject != null)
                    {
                        step.FaceObject.SetActive(true);
                        step.NowFaceObject.SetActive(false);
                        Debug.Log($"カウント {step.triggerCount} に到達。{step.FaceObject.name} を表示！");
                    }

                    if (step.StillObject != null)
                    {
                        step.StillObject.SetActive(true);
                        Debug.Log($"カウント {step.triggerCount} に到達。{step.StillObject.name} を表示！");
                    }
                }
            }
            if (messageIndex < messages.Length)
            {
                StartCoroutine(TypeMessage(messages[messageIndex]));
            }
            else
            {
                SceneManager.LoadScene(StageName);
            }
        }
    }

    IEnumerator TypeMessage(string message)
    {
        isTyping = true;
        messageText.text = "";

        foreach (char c in message)
        {
            messageText.text += c;
            yield return new WaitForSeconds(charDelay);
        }

        isTyping = false;
    }

}
