using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void OnEnable()
    {
        InputManager.Instance.actions.Player.SeaSkill.started+=ActivateSeaSkill;
        InputManager.Instance.actions.Player.ChikyuSkill.started += ActivateChikyuSkill;
    }
    private void OnDisable()
    {
        InputManager.Instance.actions.Player.SeaSkill.started -=ActivateSeaSkill;
        InputManager.Instance.actions.Player.ChikyuSkill.started -= ActivateChikyuSkill;
    }                                        
                                             
    void Start()
    {
        ActionMode = ActionModeType.ChikyuView;
        mainCamera.ModeChanged();
    }
    void Update()
    {/*
            if (Input.GetKeyDown(KeyCode.X))
            {
                ActivateChikyuSkill();
            }

            //UtyuSkillオンオフ
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ActivateSeaSkill();
            }
        */
    }

    public void ChangeActionMode(ActionModeType a, ActionModeType b)
    {
        //Debug.Log("配布"); 
        //Debug.Log(ActionModeChangeEvent?.GetInvocationList().Length);
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

    private void ActivateChikyuSkill(InputAction.CallbackContext ctx)
    {
        if (MyGameModeController.GameMode != "Action") return;

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

    private void ActivateSeaSkill(InputAction.CallbackContext ctx)
    {
        if (MyGameModeController.GameMode != "Action") return;

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