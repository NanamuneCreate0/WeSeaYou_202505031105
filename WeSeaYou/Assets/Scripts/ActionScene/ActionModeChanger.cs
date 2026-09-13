using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionModeChanger : MonoBehaviour
{
    public enum ActionModeType
    {
        Neutral = 0,   // 地球君視点
        //ChikyuView = 0,UtyuView = 1,//Skill時、2キャラの位置が切り替わるのが嫌なら、場所と見た目をパッっと入れ替えることにしよう。なのでこれは使わない//
        ChikyuSkill = 10,
        UtyuSkill = 11,
    }

    public static event Action<ActionModeType, ActionModeType> ActionModeChangeEvent;
    public ActionModeType ActionMode = ActionModeType.Neutral;

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
        ActionMode = ActionModeType.Neutral;
        mainCamera.ModeChanged();
    }
    void Update()
    {
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

    private void ActivateChikyuSkill(InputAction.CallbackContext ctx)
    {
        if (MyGameModeController.GameMode != "Action") return;

        if (ActionMode == ActionModeType.Neutral)
        {
            ChangeActionMode(ActionModeType.ChikyuSkill, ActionModeType.Neutral);
        }
        else if (ActionMode == ActionModeType.UtyuSkill)
        {
            ChangeActionMode(ActionModeType.ChikyuSkill, ActionModeType.UtyuSkill);
        }
        else if (ActionMode == ActionModeType.ChikyuSkill)
        {
            ChangeActionMode(ActionModeType.Neutral, ActionModeType.ChikyuSkill);
        }
    }

    private void ActivateSeaSkill(InputAction.CallbackContext ctx)
    {
        if (MyGameModeController.GameMode != "Action") return;

        if (ActionMode == ActionModeType.Neutral)
        {
            ChangeActionMode(ActionModeType.UtyuSkill, ActionModeType.Neutral);
        }
        else if (ActionMode == ActionModeType.ChikyuSkill)
        {
            ChangeActionMode(ActionModeType.UtyuSkill, ActionModeType.ChikyuSkill);
        }
        else if (ActionMode == ActionModeType.UtyuSkill)
        {
            ChangeActionMode(ActionModeType.Neutral, ActionModeType.UtyuSkill);
        }
    }
}