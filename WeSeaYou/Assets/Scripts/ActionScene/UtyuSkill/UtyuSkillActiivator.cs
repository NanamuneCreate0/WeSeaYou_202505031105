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
            if (MyGameModeController.GameMode == "Action")
            {
                if (MyActionModeChanger.ActionMode == 1)
                {
                    //MyGameModeController.GameMode = "UtyuSkill";
                    MyActionModeChanger.ActionMode = 11;
                    MyUtyuSkillUI.SetActive(true);
                    MyUtyuSkillHand.ActivationStart();
                }
                else if (MyActionModeChanger.ActionMode == 11) // (MyGameModeController.GameMode == "UtyuSkill")
                {
                    //MyGameModeController.GameMode = "Action";
                    MyActionModeChanger.ActionMode = 1;
                    MyUtyuSkillHand.ConfirmStaticItemList(true);
                    MyItemDisplayer.SetItemDisplay(true);
                    MyUtyuSkillHand.HilightStart = 0;
                    MyUtyuSkillUI.SetActive(false);
                }
            }
        }
    }
}
