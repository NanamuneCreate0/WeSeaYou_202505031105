using UnityEngine;

public class SpeechBubleDirector : MonoBehaviour
{
    [SerializeField]
    GameObject SpeechBubble;
    [SerializeField]
    GameObject message;

    public static bool isStartCoroutine;
    [SerializeField]
    int[] TrrigerID;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && SpeechBubbleTrriggerScript.isEnter == true && TrrigerID[SpeechBubbleTrriggerScript.TriggerNumbersensor] == 0)
        {
            SpeechBubble.SetActive(true);
            message.SetActive(true);
            Debug.Log(SpeechBubbleTrriggerScript.TriggerNumbersensor);
            isStartCoroutine = true;
            TrrigerID[SpeechBubbleTrriggerScript.TriggerNumbersensor] = 1;
        }
    }

}
