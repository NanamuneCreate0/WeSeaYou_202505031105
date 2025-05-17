using UnityEngine;

public class ChikyuSkillController : MonoBehaviour
{
    [SerializeField]
    GameModeController MyGameModeController;
    [SerializeField]
    GameObject MyChikyuSkillUI;

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
