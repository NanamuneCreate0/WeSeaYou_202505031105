using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class ChikyuWalk : MonoBehaviour
{
    public enum Direction
    {
        Left = -1,
        None = 0,
        Right = 1
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float resistance;
    [SerializeField] float power;
    [SerializeField] float jumpPower;
    [SerializeField] Animator animator;

    Rigidbody2D rb;
    Collider2D myCol;
    // ê⁄ínèÛë‘
    public bool IsGrounding { get; private set; }
    public Collider2D CurrentGroundCollider { get; private set; }
    public Vector2 GroundPoint { get; private set; }

    Direction direction = Direction.None;
    public Direction LastDirection= Direction.Right;

float inputX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        myCol = GetComponentInChildren<Collider2D>();
    }

    void OnEnable()
    {
        InputManager.Instance.actions.Player.Move.performed += OnMove;
        InputManager.Instance.actions.Player.Move.canceled += OnMove;
        InputManager.Instance.actions.Player.Jump.performed += OnJump;
    }

    void OnDisable()
    {
        InputManager.Instance.actions.Player.Move.performed -= OnMove;
        InputManager.Instance.actions.Player.Move.canceled -= OnMove;
        InputManager.Instance.actions.Player.Jump.performed -= OnJump;
    }

    void FixedUpdate()
    {
        UpdateGrounding();
        UpdateDirection();
        ApplyMovement();
    }

    //ê⁄ínîªíË
    void UpdateGrounding()
    {
        IsGrounding = GroundUtil.CheckGrounded(
            myCol,
            out Collider2D col,
            out Vector2 point);

        if (IsGrounding)
        {
            CurrentGroundCollider = col;
            GroundPoint = point;
        }
    }

    //InputSystem
    void OnMove(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }
    void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounding)
        {
            rb.AddForce(Vector2.up * jumpPower *rb.mass);
        }
    }


    void UpdateDirection()
    {
        Direction newDirection = Direction.None;

        if (inputX > 0)
        {
            newDirection = Direction.Right;
            LastDirection = Direction.Right;
        }
        else if (inputX < 0)
        {
            newDirection = Direction.Left;
            LastDirection = Direction.Left;
        }

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

        float groundVelocityX = 0f;
        if (GroundUtil.CheckGrounded(
            myCol,
            out Collider2D groundCol,
            out Vector2 hitPoint))
        {
            IVelocityProvider provider = groundCol.GetComponent<IVelocityProvider>();
            if (provider != null)
            {
                groundVelocityX = provider.Velocity.x;
                Debug.Log(provider);
            }
        }

        rb.AddForce(Vector2.right * inputX * moveSpeed);
        float resistanceForce = -(rb.linearVelocityX - groundVelocityX) * Mathf.Pow(resistance,power);
        rb.AddForce(Vector2.right * resistanceForce);
        animator.SetFloat("AnimSpeed", Mathf.Abs(inputX));
    }

}