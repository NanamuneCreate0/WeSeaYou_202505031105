using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public PlayerInputActions actions;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        actions = new PlayerInputActions();
    }

    void OnEnable()
    {
        actions.Player.Enable();
        actions.UI.Enable();
    }

    void OnDisable()
    {
        if (actions == null) return;
        actions.Player.Disable();
        actions.UI.Disable();
    }
}
//Vector2 move =InputManager.Instance.Actions.Player.Move.ReadValue<Vector2>();
//InputManager.Instance.Actions.Player.Jump.performed += OnJump;


