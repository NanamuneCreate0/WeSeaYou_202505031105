using UnityEngine;
using System;

public class ChikyuSkillActivator : MonoBehaviour
{
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
        Debug.Log(a + "地球うけとり" + b);
        
        if(a== ActionModeChanger.ActionModeType.ChikyuSkill)
        {
            Debug.Log("ChikyuSkillはじめ");
            if(b!= ActionModeChanger.ActionModeType.ChikyuView&& b != ActionModeChanger.ActionModeType.UtyuSkill) { Debug.LogWarning("想定外のActionMode変更"); }
            MyChikyuSkillUI.SetActive(true);
            MyChikyuSkillTable.ActivationStart();
            MyChikyuSkillHand.ActivationStart();//Tableへの提出があるためHandが後。提出だけLateActivationStart()に分けてもいい
        }
        if (b == ActionModeChanger.ActionModeType.ChikyuSkill)
        {
            Debug.Log("ChikyuSkillおわり");
            if (a != ActionModeChanger.ActionModeType.ChikyuView && a != ActionModeChanger.ActionModeType.UtyuSkill) { Debug.LogWarning("想定外のActionMode変更"); }
            //MyItemDisplayer.SetItemDisplay(true);
            MyChikyuSkillHand.HilightStart = 0;
            MyChikyuSkillUI.SetActive(false);
        }
    }
}
