using UnityEngine;

public class UtyuSkillActivator : MonoBehaviour
{
    [SerializeField]
    GameModeController MyGameModeController;
    [SerializeField]
    ActionModeChanger MyActionModeChanger;
    [SerializeField]
    GameObject MyUtyuSkillUI;
    [SerializeField]
    UtyuSkillHand MyUtyuSkillHand;
    private void OnEnable()
    {
        ActionModeChanger.ActionModeChangeEvent += GetActionModeChange;
    }
    private void OnDisable()
    {
        ActionModeChanger.ActionModeChangeEvent -= GetActionModeChange;
    }
    void GetActionModeChange(ActionModeChanger.ActionModeType a, ActionModeChanger.ActionModeType b)
    {
        if (a == ActionModeChanger.ActionModeType.UtyuSkill && b == ActionModeChanger.ActionModeType.ChikyuView)
        {
            MyUtyuSkillUI.SetActive(true);
            MyUtyuSkillHand.ActivationStart();
        }
        else if (a == ActionModeChanger.ActionModeType.ChikyuView && b == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            MyUtyuSkillHand.OnDisableAndReset();
            MyUtyuSkillUI.SetActive(false);
        }
    }

    void Update()
    {
    }
}
