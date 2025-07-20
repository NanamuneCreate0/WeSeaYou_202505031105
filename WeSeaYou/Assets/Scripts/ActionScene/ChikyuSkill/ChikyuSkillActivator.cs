using UnityEngine;

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

    void Start()
    {
        
    }

    void Update()
    {
        //Menuèoåª
        if (Input.GetKeyDown(KeyCode.X))
        {
            if(MyGameModeController.GameMode == "Action")
            {
                if (MyActionModeChanger.ActionMode == 0)
                {
                    //MyGameModeController.GameMode = "ChikyuSkill";
                    MyActionModeChanger.ActionMode = 10;
                    MyChikyuSkillUI.SetActive(true);
                    MyChikyuSkillHand.ActivationStart();
                    MyChikyuSkillTable.ActivationStart();
                }
                else if (MyActionModeChanger.ActionMode == 10) // (MyGameModeController.GameMode == "ChikyuSkill")
                {
                    //MyGameModeController.GameMode = "Action";
                    MyActionModeChanger.ActionMode = 0;
                    MyChikyuSkillHand.ConfirmStaticItemList(true);
                    MyItemDisplayer.SetItemDisplay(true);
                    MyChikyuSkillHand.HilightStart = 0;
                    MyChikyuSkillUI.SetActive(false);
                }
            }
        }
    }
}
