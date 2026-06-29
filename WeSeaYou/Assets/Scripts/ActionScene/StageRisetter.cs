using UnityEngine;
using UnityEngine.InputSystem;

public class StageRisetter : MonoBehaviour
{
    void OnEnable()
    {
        InputManager.Instance.actions.Player.Jump.performed += OnReset;
    }

    void OnDisable()
    {
        InputManager.Instance.actions.Player.Jump.performed -= OnReset;
    }
    void OnReset(InputAction.CallbackContext context)
    {

    }
}
