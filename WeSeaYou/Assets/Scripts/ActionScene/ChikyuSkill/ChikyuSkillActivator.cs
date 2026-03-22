using UnityEngine;
using System;

public class ChikyuSkillActivator : MonoBehaviour
{
    [SerializeField]
    GameModeController MyGameModeController;
    [SerializeField]
    ActionModeChanger MyActionModeChanger;
    [SerializeField]
    GameObject MyChikyuSkillUI;
    [SerializeField]
    ChikyuSkillHand MyChikyuSkillHand;
    [SerializeField]
    ChikyuSkillTable MyChikyuSkillTable;
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
        if(a== ActionModeChanger.ActionModeType.ChikyuSkill&& b== ActionModeChanger.ActionModeType.ChikyuView)
        {
            MyChikyuSkillUI.SetActive(true);
            MyChikyuSkillTable.ActivationStart();
            MyChikyuSkillHand.ActivationStart();//TableÇ÷ÇÃíÒèoÇ™Ç†ÇÈÇΩÇﬂHandÇ™å„ÅBíÒèoÇæÇØLateActivationStart()Ç…ï™ÇØÇƒÇ‡Ç¢Ç¢
        }
        if (a == ActionModeChanger.ActionModeType.ChikyuView && b == ActionModeChanger.ActionModeType.ChikyuSkill)
        {
            //MyItemDisplayer.SetItemDisplay(true);
            MyChikyuSkillHand.HilightStart = 0;
            MyChikyuSkillUI.SetActive(false);
        }
    }
}
