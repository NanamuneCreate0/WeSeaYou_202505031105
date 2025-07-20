using UnityEngine;

public class ActionModeChanger : MonoBehaviour
{
    public int ActionMode=0;//0:地球君視点//1:宇宙君視点//2:おんぶ//3:手つなぎ//10:地球スキル//11:宇宙スキル
    [SerializeField]
    GameObject MyUtyu;
    [SerializeField]
    GameObject MyChikyu;
    [SerializeField]
    MainCamera mainCamera;
    [SerializeField]
    GameModeController MyGameModeController;
    void Start()
    {
        ActionMode = 0;
        mainCamera.ModeChanged();
        MyChikyu.GetComponent<ChikyuWalk>().ModeChanged();
        MyUtyu.GetComponent<UtyuWalk>().ModeChanged();
    }
    void Update()
    {
        //キャラの切り替え
        if (Input.GetKeyDown(KeyCode.Q)&&MyGameModeController.GameMode=="Action")
        {
            if (ActionMode == 0)
            {
                ActionMode = 1;
            }
            else if (ActionMode == 1)
            {
                ActionMode = 0;
            }
            mainCamera.ModeChanged();
            MyUtyu.GetComponent<UtyuWalk>().ModeChanged();
            MyChikyu.GetComponent<ChikyuWalk>().ModeChanged();
        }
    }
}
