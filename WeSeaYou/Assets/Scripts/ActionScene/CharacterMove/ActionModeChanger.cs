using System;
using UnityEngine;

public class ActionModeChanger : MonoBehaviour
{
    public enum ActionModeType
    {
        ChikyuView = 0,   // 地球君視点
        UtyuView = 1,     // 宇宙君視点（今回未使用だけど残すならOK）
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
        //キャラの切り替え
        if (MyGameModeController.GameMode == "Action")
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (ActionMode == ActionModeType.ChikyuView)
                {
                    ChangeActionMode(ActionModeType.ChikyuSkill, ActionModeType.ChikyuView);
                }
                else if (ActionMode == ActionModeType.ChikyuSkill)
                {
                    ChangeActionMode(ActionModeType.ChikyuView, ActionModeType.ChikyuSkill);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (ActionMode == ActionModeType.ChikyuView)
                {
                    ChangeActionMode(ActionModeType.UtyuSkill, ActionModeType.ChikyuView);
                }
                else if (ActionMode == ActionModeType.UtyuSkill)
                {
                    ChangeActionMode(ActionModeType.ChikyuView, ActionModeType.UtyuSkill);
                }
                else if (ActionMode == ActionModeType.UtyuSkillActive)
                {
                    ChangeActionMode(ActionModeType.UtyuSkill, ActionModeType.UtyuSkillActive);
                }
            }
        }
    }

    public void ChangeActionMode(ActionModeType a, ActionModeType b)
    {
        if (ActionMode == b)
        {
            ActionMode = a;
        }
        else
        {
            Debug.LogWarning("ChangeActionMode Failed");
        }

        ActionModeChangeEvent?.Invoke(a, b);
    }
}