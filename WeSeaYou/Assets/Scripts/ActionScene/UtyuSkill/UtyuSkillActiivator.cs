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
        if (a == 11 && b == 0)
        {
            MyUtyuSkillUI.SetActive(true);
            MyUtyuSkillHand.ActivationStart();
        }
        if (a == 0 && b == 11)
        {
            MyUtyuSkillHand.OnDisableAndReset();
            MyUtyuSkillUI.SetActive(false);
        }
    }

    void Update()
    {
    }
}
