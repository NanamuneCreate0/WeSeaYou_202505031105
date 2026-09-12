using UnityEngine;
using UnityEngine.InputSystem;

public class ChikyuWalk : MonoBehaviour//////Chikyuと書いてるけど、実際にはどっちのキャラか分からない　操作しない方
{
    public enum Direction
    {
        Left = -1,
        None = 0,
        Right = 1
    }
    public string CurrentAnim;

    [SerializeField] float moveSpeed;
    [SerializeField] float resistance;
    [SerializeField] float power;
    [SerializeField] float jumpPower;
    [SerializeField] Animator animator;

    Rigidbody2D rb;
    Collider2D myCol;
    bool wasGrounding;
    bool groundingInitialized; 
    float previousVelocityY;

    // 接地状態
    public bool IsGrounding { get; private set; }
    public Collider2D CurrentGroundCollider { get; private set; }
    public Vector2 GroundPoint { get; private set; }

    Direction? lastDirection = null;
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
        UpdateAirState();//空中アニメを反映
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
        //着地アニメ
        
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
            lastDirection = null;
            UpdateDirection();
        }
        wasGrounding = IsGrounding;
    }
    void UpdateAirState()
    {
        if (IsGrounding)
        {
            previousVelocityY = rb.linearVelocityY;
            return;
        }

        if (previousVelocityY >= 0 && rb.linearVelocityY < 0)
        {
            lastDirection = null;
            UpdateDirection();
        }
        previousVelocityY = rb.linearVelocityY;
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
                PlayAnimation("WaitRight");
            }
            else if (CurrentDirection == Direction.Left)
            {
                PlayAnimation("WaitLeft");
            }

            return;
        }

        // 地上でDirection.None以外なら待機
        switch (CurrentDirection)
        {
            case Direction.Right:
                PlayAnimation("WalkRight");
                break;

            case Direction.Left:
                PlayAnimation("WalkLeft");
                break;
        }
    }
    void PlayJumpAnimation()
    {
        if (rb.linearVelocityY >= 0)
        {
            switch (CurrentDirection)
            {
                case Direction.Right:
                    PlayAnimation("JumpRight");
                    break;

                case Direction.Left:
                    PlayAnimation("JumpLeft");
                    break;
            }
        }
        else
        {
            switch (CurrentDirection)
            {
                case Direction.Right:
                    PlayAnimation("FallRight");
                    break;

                case Direction.Left:
                    PlayAnimation("FallLeft");
                    break;
            }
        }
    }
    void PlayAnimation(string animationName)
    {
        CurrentAnim = animationName;
        animator.Play(animationName);
    }
}