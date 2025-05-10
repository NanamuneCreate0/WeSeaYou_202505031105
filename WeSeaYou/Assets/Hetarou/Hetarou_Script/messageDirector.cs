using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UItext : MonoBehaviour
{
    public TMP_Text messageText;            // �\���pText
    public string[] messages;           // �\�����������̓��X�g
    public float charDelay = 0.05f;     // ��������̑���

    public int messageIndex;
    public int messageLength;
    public string StageName;
    private bool isTyping = false;


    void Start()
    {


        switch (PublicStaticStatus.CurrentStage)
        {
            case 0:
                messageIndex = 0;
                messageLength = 2;
                StageName = "ActionScene01";
                break;

            case 1:
                messageIndex = 2;
                messageLength = 4;
                StageName = "ActionScene02";
                break;
        }
        StartCoroutine(TypeMessage(messages[messageIndex]));


    }

    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            messageIndex++;

            if (messageIndex < messageLength)
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
