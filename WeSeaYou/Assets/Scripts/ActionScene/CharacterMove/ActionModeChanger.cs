using System;
using UnityEngine;

public class ActionModeChanger : MonoBehaviour
{
    public enum ActionModeType
    {
        ChikyuView = 0,   // 地球君視点
        UtyuView = 1,     // 宇宙君視点（今回未使用だけど一応）
        ChikyuSkill = 10,
        UtyuSkill = 11,
        UtyuSkillActive = 21
    }

    public static event Action<ActionModeType, ActionModeType> ActionModeChangeEvent;
    public ActionModeType ActionMode = ActionModeType.ChikyuView;

    [SerializeField]
    MainCamera mainCamera;
    [SerializeField]
    GameModeController MyGameModeController;
    void Start()
    {
        ActionMode = ActionModeType.ChikyuView;
        mainCamera.ModeChanged();
    }
    void Update()
    {
        if (MyGameModeController.GameMode == "Action")
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                //Debug.Log("x");
                if (ActionMode == ActionModeType.ChikyuView)
                {
                    ChangeActionMode(ActionModeType.ChikyuSkill, ActionModeType.ChikyuView);
                }
                else if (ActionMode == ActionModeType.UtyuSkill)
                {
                    ChangeActionMode(ActionModeType.ChikyuSkill, ActionModeType.UtyuSkill);
                }
                else if (ActionMode == ActionModeType.ChikyuSkill)
                {
                    ChangeActionMode(ActionModeType.ChikyuView, ActionModeType.ChikyuSkill);
                }
            }

            //UtyuSkillオンオフ
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //Debug.Log("z");
                if (ActionMode == ActionModeType.ChikyuView)
                {
                    ChangeActionMode(ActionModeType.UtyuSkill, ActionModeType.ChikyuView);
                }
                else if (ActionMode == ActionModeType.ChikyuSkill)
                {
                    ChangeActionMode(ActionModeType.UtyuSkill, ActionModeType.ChikyuSkill);
                }
                else if (ActionMode == ActionModeType.UtyuSkill)
                {
                    ChangeActionMode(ActionModeType.ChikyuView, ActionModeType.UtyuSkill);
                }
            }
        }
    }

    public void ChangeActionMode(ActionModeType a, ActionModeType b)
    {
        Debug.Log("配布"); 
        Debug.Log(ActionModeChangeEvent?.GetInvocationList().Length);
        ActionModeChangeEvent?.Invoke(a, b);
        if (ActionMode == b)
        {
            ActionMode = a;
        }
        else
        {
            Debug.LogWarning("ChangeActionMode Failed");
        }
    }
}