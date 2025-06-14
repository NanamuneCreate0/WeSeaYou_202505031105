using UnityEngine;

public class ChikyuSkillActivator : MonoBehaviour
{
    [SerializeField]
    GameModeController MyGameModeController;
    [SerializeField]
    GameObject MyChikyuSkillUI;
    [SerializeField]
    ChikyuSkillHand MyChikyuSkillHand;
    [SerializeField]
    ChikyuSkillTable MyChikyuSkillTable;

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
                Debug.Log("ChikyuSkill");
                MyGameModeController.GameMode = "ChikyuSkill";
                MyChikyuSkillUI.SetActive(true);
                MyChikyuSkillHand.ActivationStart();
                MyChikyuSkillTable.ActivationStart();
            }
            else if (MyGameModeController.GameMode == "ChikyuSkill")
            {
                MyGameModeController.GameMode = "Action";
                MyChikyuSkillUI.SetActive(false);
            }
        }


        if (MyGameModeController.GameMode == "ChikyuSkill")
        {
        }
    }
}
