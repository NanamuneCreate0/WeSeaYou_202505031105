using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject HandlingPlayer;
    [SerializeField]
    GameObject MyUtyu;
    [SerializeField]
    GameObject MyChikyu;
    void Start()
    {
        HandlingPlayer = MyUtyu;
        MyChikyu.GetComponent<ChikyuController>().IsHandlingPlayer = false;
        MyUtyu.GetComponent<UtyuController>().IsHandlingPlayer = true;
    }
    void Update()
    {
        //ÉLÉÉÉâÇÃêÿÇËë÷Ç¶
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (HandlingPlayer == MyUtyu)
            {
                HandlingPlayer = MyChikyu;
                MyUtyu.GetComponent<UtyuController>().IsHandlingPlayer = false;
                MyChikyu.GetComponent<ChikyuController>().IsHandlingPlayer = true;
            }
            else if (HandlingPlayer == MyChikyu)
            {
                HandlingPlayer = MyUtyu;
                MyChikyu.GetComponent<ChikyuController>().IsHandlingPlayer = false;
                MyUtyu.GetComponent<UtyuController>().IsHandlingPlayer = true;
            }
        }
    }
}
