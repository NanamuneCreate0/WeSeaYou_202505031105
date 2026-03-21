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
    [SerializeField]
    ItemDisplayer MyItemDisplayer;
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
            MyChikyuSkillHand.ActivationStart();
            MyChikyuSkillTable.ActivationStart();
        }
        if (a == ActionModeChanger.ActionModeType.ChikyuView && b == ActionModeChanger.ActionModeType.ChikyuSkill)
        {
            MyItemDisplayer.SetItemDisplay(true);
            MyChikyuSkillHand.HilightStart = 0;
            MyChikyuSkillUI.SetActive(false);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
    }
}
