using UnityEngine;
using UnityEngine.InputSystem;

public class SeaSkillActivator : MonoBehaviour
{
    [SerializeField] private SeaSkillExecuter seaSkillExecuter;

    private bool isInSpecialMode = false;

    private void OnSkillStarted(InputAction.CallbackContext _) => ModeChanger();

    void OnEnable()
    {
        if (InputManager.Instance == null || InputManager.Instance.actions == null) return;
        InputManager.Instance.actions.UI.ModeChanger.started += OnSkillStarted;
    }

    void OnDisable()
    {
        if (InputManager.Instance == null || InputManager.Instance.actions == null) return;
        InputManager.Instance.actions.UI.ModeChanger.started -= OnSkillStarted;
    }

    private void ModeChanger()
    {
        isInSpecialMode = !isInSpecialMode;

        if (isInSpecialMode)
        {
            Debug.Log("StartSeaSkill");
            seaSkillExecuter.ActivateSkill();
        }
        else
        {
            Debug.Log("EndSeaSkill");
            seaSkillExecuter.End();
        }
    }

}
