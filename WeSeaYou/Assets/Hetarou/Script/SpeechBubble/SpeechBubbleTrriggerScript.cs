using UnityEngine;

public class SpeechBubbleTrriggerScript : MonoBehaviour
{
    [SerializeField]
    int TriggerNumber;
    public static int TriggerNumbersensor;
    public static bool isEnter;
    public static int messageLength;
    public static int messageIndex;

    public int key = 0;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        key++;
        TriggerNumbersensor = TriggerNumber;
        switch(TriggerNumbersensor)
        {
            case 0:
                messageIndex = 0;
                messageLength = 4;
                break;
            case 1:
                messageIndex = 4;
                messageLength = 8;
                break;
        }
        if(key == 4)isEnter = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        key--;
        isEnter = false;
    }
}
