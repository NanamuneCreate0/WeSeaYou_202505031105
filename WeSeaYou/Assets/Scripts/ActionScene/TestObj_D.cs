using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestObj_D : MonoBehaviour
{
    void OnEnable()
    {
        InputManager.Instance.actions.Player.Jump.performed += OnJump;
        //InputManager.Instance.actions.UI.Submit.performed += OnSubmit;
    }

    void OnDisable()
    {
        InputManager.Instance.actions.Player.Jump.performed -= OnJump;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump pressed");
    }
}