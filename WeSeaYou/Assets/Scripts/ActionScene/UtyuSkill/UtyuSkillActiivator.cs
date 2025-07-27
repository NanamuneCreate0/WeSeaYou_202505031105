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
        Debug.Log("get");
        if (a == 11 && b == 1)
        {
            MyUtyuSkillUI.SetActive(true);
            MyUtyuSkillHand.ActivationStart();
        }
        if (a == 1 && b == 11)
        {
            MyUtyuSkillHand.ConfirmStaticItemList(true);
            MyItemDisplayer.SetItemDisplay(true);
            MyUtyuSkillHand.HilightStart = 0;
            MyUtyuSkillUI.SetActive(false);
        }
    }

    void Update()
    {
        /*
        //Menuèoåª
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (MyGameModeController.GameMode == "Action")
            {
                if (MyActionModeChanger.ActionMode == 1)
                {
                    MyActionModeChanger.ChangeActionMode(11, 1);
                    //MyUtyuSkillUI.SetActive(true);
                    //MyUtyuSkillHand.ActivationStart();
                }
                else if (MyActionModeChanger.ActionMode == 11)
                {
                    MyActionModeChanger.ChangeActionMode(1, 11);
                    //MyUtyuSkillHand.ConfirmStaticItemList(true);
                    //MyItemDisplayer.SetItemDisplay(true);
                    //MyUtyuSkillHand.HilightStart = 0;
                    //MyUtyuSkillUI.SetActive(false);
                }
            }
        }*/
    }
}
