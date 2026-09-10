/*using UnityEngine;
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
    // 接地状態
    public bool IsGrounding { get; private set; }
    public Collider2D CurrentGroundCollider { get; private set; }
    public Vector2 GroundPoint { get; private set; }

    Direction lastDirection = Direction.None;
    public Direction CurrentDirection= Direction.Right;

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

    //接地判定
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
            CurrentDirection = Direction.Right;
        }
        else if (inputX < 0)
        {
            newDirection = Direction.Left;
            CurrentDirection = Direction.Left;
        }

            if (newDirection == lastDirection) return;

        switch (newDirection)
        {
            case Direction.Right:
                animator.Play("UtyuWalkRight");
                break;
            case Direction.Left:
                animator.Play("UtyuWalkLeft");
                break;
            case Direction.None:
                if (lastDirection == Direction.Right)
                    animator.Play("UtyuWaitRight");
                else if (lastDirection == Direction.Left)
                    animator.Play("UtyuWaitRight");/////////////////
                break;
        }

        lastDirection = newDirection;
    }
    
    void ApplyMovement()
    {
        float groundVelocityX = 0f;

        //動く足場に乗ってる場合の修正
        if (IsGrounding && CurrentGroundCollider != null)
        {
            IVelocityProvider provider =CurrentGroundCollider.GetComponent<IVelocityProvider>();
            if (provider != null)
            {
                groundVelocityX = provider.Velocity.x;
            }
        }

        float moveMultiplier = IsGrounding ? 1f : 0.7f;
        rb.AddForce(Vector2.right * inputX * moveSpeed * moveMultiplier);

        float resistanceForce =-(rb.linearVelocityX - groundVelocityX) * Mathf.Pow(resistance, power);
        rb.AddForce(Vector2.right * resistanceForce);

        animator.SetFloat("AnimSpeed", Mathf.Abs(inputX));
    }

}*/

using UnityEngine;
using UnityEngine.InputSystem;

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
    bool wasGrounding;
bool groundingInitialized;

    // 接地状態
    public bool IsGrounding { get; private set; }
    public Collider2D CurrentGroundCollider { get; private set; }
    public Vector2 GroundPoint { get; private set; }

    Direction lastDirection = Direction.None;
    public Direction CurrentDirection = Direction.Right;

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
        ApplyMovement();
    }

    // InputSystem
    void OnMove(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;

        UpdateDirection();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (!IsGrounding) return;

        rb.AddForce(Vector2.up * jumpPower * rb.mass);

        PlayJumpAnimation();
    }

    // 接地判定
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
        else
        {
            CurrentGroundCollider = null;
        }
        // 初回は着地扱いにしない
        if (!groundingInitialized)
        {
            wasGrounding = IsGrounding;
            groundingInitialized = true;
            return;
        }

        // 空中 → 接地になった瞬間
        if (!wasGrounding && IsGrounding)
        {
            PlayLandingAnimation();
        }

        wasGrounding = IsGrounding;
    }
    void ApplyMovement()
    {
        float groundVelocityX = 0f;

        if (IsGrounding && CurrentGroundCollider != null)
        {
            IVelocityProvider provider =
                CurrentGroundCollider.GetComponent<IVelocityProvider>();

            if (provider != null)
            {
                groundVelocityX = provider.Velocity.x;
            }
        }

        float moveMultiplier = IsGrounding ? 1f : 0.7f;

        rb.AddForce(
            Vector2.right * inputX * moveSpeed * moveMultiplier
        );

        float resistanceForce =
            -(rb.linearVelocityX - groundVelocityX)
            * Mathf.Pow(resistance, power);

        rb.AddForce(Vector2.right * resistanceForce);

        animator.SetFloat("AnimSpeed", Mathf.Abs(inputX));
    }



    // 向きの変更
    void UpdateDirection()
    {
        Direction newDirection = Direction.None;

        if (inputX > 0)
        {
            newDirection = Direction.Right;
        }
        else if (inputX < 0)
        {
            newDirection = Direction.Left;
        }

        // 向き・入力状態が変わっていなければ何もしない
        if (newDirection == lastDirection)
        {
            return;
        }

        lastDirection = newDirection;
        // None以外なら外部公開用のCurrentDirectionを更新
        if (newDirection != Direction.None)
        {
            CurrentDirection = newDirection;
        }


        // 空中ならジャンプアニメーションの向きだけ変更
        if (!IsGrounding)
        {
            PlayJumpAnimation();
            return;
        }

        // 地上でDirection.Noneなら待機
        if (newDirection == Direction.None)
        {
            if (CurrentDirection == Direction.Right)
            {
                animator.Play("UtyuWaitRight");
            }
            else if (CurrentDirection == Direction.Left)
            {
                animator.Play("UtyuWaitRight");////////////////////////////////////////////要修正
            }

            return;
        }

        // 地上でDirection.None以外なら待機
        switch (CurrentDirection)
        {
            case Direction.Right:
                animator.Play("UtyuWalkRight");
                break;

            case Direction.Left:
                animator.Play("UtyuWalkLeft");
                break;
        }
    }
    void PlayJumpAnimation()
    {
        switch (CurrentDirection)
        {
            case Direction.Right:
                animator.Play("UtyuJumpRight");
                break;

            case Direction.Left:
                animator.Play("UtyuJumpLeft");
                break;
        }
    }
    void PlayLandingAnimation()
    {
        /*
        if (lastDirection != Direction.None)
        {
            return;
        }*/

        switch (CurrentDirection)
        {
            case Direction.Right:
                animator.Play("UtyuLandRight");
                break;

            case Direction.Left:
                animator.Play("UtyuLandLeft");
                break;
        }
    }
}