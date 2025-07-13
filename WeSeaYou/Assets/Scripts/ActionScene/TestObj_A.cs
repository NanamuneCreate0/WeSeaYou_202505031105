using UnityEngine;
using UnityEngine.InputSystem;

public class TestObj_A : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log(context.phase);
            Debug.Log("ƒWƒƒƒ“ƒv‚µ‚Ü‚µ‚½");
        }
    }
}