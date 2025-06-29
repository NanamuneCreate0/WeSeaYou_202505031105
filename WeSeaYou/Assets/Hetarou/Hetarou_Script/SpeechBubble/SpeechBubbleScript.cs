using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class SpeechBubbleScript : MonoBehaviour
{
    public TMP_Text messageText;            // 表示用Text
    [SerializeField, TextArea(1, 3)]
    public string[] messages;           // 表示したい文章リスト
    public float charDelay = 0.05f;     // 文字送りの速さ

    private bool isTyping = false;

    [SerializeField]
    GameObject Chikyu;
    [SerializeField]
    GameObject Utyu;
    [SerializeField]
    GameObject SpeechBubble;

    Vector2 ChikyuPos;
    Vector2 UtyuPos;

    void Start()
    {
        
    }

    void Update()
    {
        ChikyuPos = Chikyu.transform.position;
        UtyuPos = Utyu.transform.position;

        Chikyu.GetComponent<ChikyuWalk>().enabled = false;
        Utyu.GetComponent<UtyuWalk>().enabled = false;
        if (SpeechBubleDirector.isStartCoroutine == true)
        {
            StartCoroutine(TypeMessage(messages[SpeechBubbleTrriggerScript.messageIndex]));
            SpeechBubleDirector.isStartCoroutine = false;
        }

        if (Input.GetKeyDown(KeyCode.C) && !isTyping)
        {
            SpeechBubbleTrriggerScript.messageIndex++;

            if (SpeechBubbleTrriggerScript.messageIndex < SpeechBubbleTrriggerScript.messageLength)
            {
                StartCoroutine(TypeMessage(messages[SpeechBubbleTrriggerScript.messageIndex]));
                switch(SpeechBubbleTrriggerScript.messageIndex)
                {
                    case 0:
                    case 1:
                    
                        SpeechBubble.GetComponent<RectTransform>().anchoredPosition 
                            = new Vector2(ChikyuPos.x * 50 + 240, ChikyuPos.y * 50 + 100);
                        break;
                }
            }
            //元に戻す
            else
            {
                SpeechBubble.SetActive(false);
                gameObject.SetActive(false);
                Chikyu.GetComponent<ChikyuWalk>().enabled = true;
                Utyu.GetComponent<UtyuWalk>().enabled = true;
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