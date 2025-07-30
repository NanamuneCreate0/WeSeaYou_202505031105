using System;
using UnityEngine;

public class ActionModeChanger : MonoBehaviour
{
    public static event Action<int,int> ActionModeChangeEvent;

    public int ActionMode=0;//0:地球君視点//1:宇宙君視点//2:おんぶ//3:手つなぎ//10:地球スキル//11:宇宙スキル//21:宇宙スキル機能中
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
        if (MyGameModeController.GameMode == "Action")
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (ActionMode == 0)
                {
                    ChangeActionMode(1, 0);
                }
                else if (ActionMode == 1)
                {
                    ChangeActionMode(0, 1);
                }
                mainCamera.ModeChanged();
                MyUtyu.GetComponent<UtyuWalk>().ModeChanged();
                MyChikyu.GetComponent<ChikyuWalk>().ModeChanged();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (ActionMode == 0)
                {
                    ChangeActionMode(10, 0);
                }
                else if (ActionMode == 10)
                {
                    ChangeActionMode(0, 10);
                }

                else if (ActionMode == 1)
                {
                    ChangeActionMode(11, 1);
                }
                else if (ActionMode == 11)
                {
                    ChangeActionMode(1, 11);
                }

                //ChangeActionMode(21,11)はUtyuSkillOutputで遷移
                else if (ActionMode == 21)
                {
                        ChangeActionMode(11, 21);
                }


            }
        }
    }

    public void ChangeActionMode(int a,int b)
    {
        if(ActionMode==b)
        {
            ActionMode = a;
        }
        else
        {
            Debug.LogWarning("ChangeActionMode Failed");
        }
        ActionModeChangeEvent?.Invoke(a,b);
    }
}
