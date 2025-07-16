using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public int ControlMode=0;//0:地球君視点//1:宇宙君視点//2:おんぶ//3:手つなぎ
    ///public GameObject HandlingPlayer;
    [SerializeField]
    GameObject MyUtyu;
    [SerializeField]
    GameObject MyChikyu;
    [SerializeField]
    MainCamera mainCamera;
    void Start()
    {
        /*
        HandlingPlayer = MyChikyu;
        MyChikyu.GetComponent<ChikyuWalk>().IsHandlingPlayer = true;
        MyUtyu.GetComponent<UtyuWalk>().IsHandlingPlayer = false;*/
        ControlMode = 0;
        mainCamera.ModeChanged();
        MyChikyu.GetComponent<ChikyuWalk>().ModeChanged();
        MyUtyu.GetComponent<UtyuWalk>().ModeChanged();
    }
    void Update()
    {
        //キャラの切り替え
        if (Input.GetKeyDown(KeyCode.Q))
        {
            /*if (HandlingPlayer == MyUtyu)
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
            }*/
            if (ControlMode == 0)
            {
                ControlMode = 1;
            }
            else if (ControlMode == 1)
            {
                ControlMode = 0;
            }
            mainCamera.ModeChanged();
            MyUtyu.GetComponent<UtyuWalk>().ModeChanged();
            MyChikyu.GetComponent<ChikyuWalk>().ModeChanged();
        }
    }
}
