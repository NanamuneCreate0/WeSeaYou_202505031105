using UnityEngine;
using UnityEngine.InputSystem;
using static ActionModeChanger;

public class SeaSkillActivator : MonoBehaviour
{
    [SerializeField] private SeaSkillExecuter seaSkillExecuter;
    [SerializeField] private ActionModeChanger actionModeChanger;

    private bool isInSpecialMode = false;

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
        Debug.Log(a + "宇宙うけとり" + b);
        
        if (a == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            Debug.Log("UtyuSkillはじめ");
            if (b != ActionModeChanger.ActionModeType.ChikyuView && b != ActionModeChanger.ActionModeType.ChikyuSkill) { Debug.LogWarning("想定外のActionMode変更"); }
            seaSkillExecuter.ActivateSkill();
        }
        if (b == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            Debug.Log("UtyuSkillおわり");
            if (a != ActionModeChanger.ActionModeType.ChikyuView && a != ActionModeChanger.ActionModeType.ChikyuSkill) { Debug.LogWarning("想定外のActionMode変更"); }
            seaSkillExecuter.End();
        }

    }
    /*private void OnSkillStarted(InputAction.CallbackContext _)
    {
        //if(actionModeChanger.ActionMode != ActionModeType.UtyuSkill)

        isInSpecialMode = !isInSpecialMode;
        if (isInSpecialMode)
        {
            Debug.Log("StartSeaSkill");
            seaSkillExecuter.ActivateSkill();
        }
        else
        {
            Debug.Log("EndSeaSkill");
            seaSkillExecuter.End();
        }
    }*/

}
