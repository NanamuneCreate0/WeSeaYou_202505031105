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
    void GetActionModeChange(int a, int b)
    {
        if(a==10&&b==0)
        {
            MyChikyuSkillUI.SetActive(true);
            MyChikyuSkillHand.ActivationStart();
            MyChikyuSkillTable.ActivationStart();
        }
        if (a == 0 && b == 10)
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
