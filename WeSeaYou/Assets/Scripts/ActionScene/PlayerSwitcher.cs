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
        MyChikyu.GetComponent<ChikyuWalk>().IsHandlingPlayer = false;
        MyUtyu.GetComponent<UtyuWalk>().IsHandlingPlayer = true;
    }
    void Update()
    {
        //ÉLÉÉÉâÇÃêÿÇËë÷Ç¶
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (HandlingPlayer == MyUtyu)
            {
                HandlingPlayer = MyChikyu;
                MyUtyu.GetComponent<UtyuWalk>().IsHandlingPlayer = false;
                MyChikyu.GetComponent<ChikyuWalk>().IsHandlingPlayer = true;
            }
            else if (HandlingPlayer == MyChikyu)
            {
                HandlingPlayer = MyUtyu;
                MyChikyu.GetComponent<ChikyuWalk>().IsHandlingPlayer = false;
                MyUtyu.GetComponent<UtyuWalk>().IsHandlingPlayer = true;
            }
        }
    }
}
