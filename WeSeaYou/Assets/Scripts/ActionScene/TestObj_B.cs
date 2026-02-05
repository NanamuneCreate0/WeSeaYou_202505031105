using UnityEngine;
using UnityEngine.InputSystem;

public class TestObj_B : MonoBehaviour
{
    private PlayerInputActions actions;

    void Awake()
    {
        actions = new PlayerInputActions();
    }

    void OnEnable()
    {
        actions.Player.Enable();
    }

    void OnDisable()
    {
        actions.Player.Disable();
    }

    void Update()
    {
        Vector2 move = actions.Player.Move.ReadValue<Vector2>();
        //Debug.Log(move);
        bool jump = actions.Player.Jump.WasPressedThisFrame();
        if(jump)
        {
            Debug.Log("jmp");
        }
    }
}
