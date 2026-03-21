using UnityEngine;
using UnityEngine.InputSystem;

public class ChikyuWalk : MonoBehaviour
{
    enum Direction
    {
        None,
        Right,
        Left
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] Animator animator;

    Rigidbody2D rb;
    IsGroundingJudger grounding;

    Direction direction = Direction.None;

    float inputX; // ì¸óÕílÇï€éù

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grounding = transform.GetChild(0).GetComponent<IsGroundingJudger>();
    }

    void OnEnable()
    {
        var player = InputManager.Instance.actions.Player;

        player.Move.performed += OnMove;
        player.Move.canceled += OnMove;

        player.Jump.performed += OnJump;
    }

    void OnDisable()
    {
        var player = InputManager.Instance.actions.Player;

        player.Move.performed -= OnMove;
        player.Move.canceled -= OnMove;

        player.Jump.performed -= OnJump;
    }

    void Update()
    {
        UpdateDirection();
        ApplyMovement();
    }

    //InputSystem
    void OnMove(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }
    void OnJump(InputAction.CallbackContext context)
    {
        if (grounding.IsGrounding)
        {
            rb.AddForce(Vector2.up * jumpPower * 20f);
        }
    }


    void UpdateDirection()
    {
        Direction newDirection = Direction.None;

        if (inputX > 0) newDirection = Direction.Right;
        else if (inputX < 0) newDirection = Direction.Left;

        if (newDirection == direction) return;

        switch (newDirection)
        {
            case Direction.Right:
                animator.SetTrigger("WalkRight");
                break;
            case Direction.Left:
                animator.SetTrigger("WalkLeft");
                break;
            case Direction.None:
                if (direction == Direction.Right)
                    animator.SetTrigger("StandRight");
                else if (direction == Direction.Left)
                    animator.SetTrigger("StandLeft");
                break;
        }

        direction = newDirection;
    }

    void ApplyMovement()
    {
        float velocityX = inputX * moveSpeed;
        rb.linearVelocityX = velocityX;

        animator.SetFloat("AnimSpeed", Mathf.Abs(inputX));
    }
}