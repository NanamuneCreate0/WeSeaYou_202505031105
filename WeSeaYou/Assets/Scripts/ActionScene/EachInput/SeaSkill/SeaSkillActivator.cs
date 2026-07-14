using UnityEngine;
using UnityEngine.InputSystem;
using static ActionModeChanger;

public class SeaSkillActivator : MonoBehaviour
{
    [SerializeField] private ActionModeChanger actionModeChanger;
    [SerializeField] private SeaSkillExecutor seaSkillExecuter;
    [SerializeField] private SeaSkillAura seaSkillAura; 


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
        if (a == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            if (b != ActionModeChanger.ActionModeType.ChikyuView && b != ActionModeChanger.ActionModeType.ChikyuSkill) { Debug.LogWarning("想定外のActionMode変更"); }
            seaSkillExecuter.ActivateSkill();
            seaSkillAura.ActivateSkill();
        }
        if (b == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            if (a != ActionModeChanger.ActionModeType.ChikyuView && a != ActionModeChanger.ActionModeType.ChikyuSkill) { Debug.LogWarning("想定外のActionMode変更"); }
            seaSkillExecuter.EndSkill();
            seaSkillAura.EndSkill();
        }

    }
}
