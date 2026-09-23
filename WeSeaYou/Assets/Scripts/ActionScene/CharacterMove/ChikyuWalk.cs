/*using UnityEngine;ダッシュ無し保存版
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
    [SerializeField] ActionModeChanger actionModeChanger;

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
        ActionModeChanger.ActionModeChangeEvent += GetActionModeChange;
    }

    void OnDisable()
    {
        InputManager.Instance.actions.Player.Move.performed -= OnMove;
        InputManager.Instance.actions.Player.Move.canceled -= OnMove;
        InputManager.Instance.actions.Player.Jump.performed -= OnJump;
        ActionModeChanger.ActionModeChangeEvent -= GetActionModeChange;
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
    //Event
    void GetActionModeChange(ActionModeChanger.ActionModeType a, ActionModeChanger.ActionModeType b)
    {
        UpdateDirection(true);
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
            UpdateDirection(true);
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
            UpdateDirection(true);
        }
        previousVelocityY = rb.linearVelocityY;
    }
    void ApplyMovement()
    {
        float groundVelocityX = 0f;

        if (IsGrounding && CurrentGroundCollider != null)
        {
            IVelocityProvider provider =
                CurrentGroundCollider.transform.parent.GetComponentInChildren<IVelocityProvider>();

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
    void UpdateDirection(bool force = false)//アニメを強制的に変える場合true
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

        // 向き・入力状態が変わっていなければ何もしない(forceならする)
        if (!force && newDirection == lastDirection)
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
        string originalAnimationName = animationName;

        if (actionModeChanger.ActionMode == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            if (animationName == "WalkRight")
            {
                animationName = "UtyuSkillWalkRight";
            }
            else if (animationName == "WalkLeft")
            {
                animationName = "UtyuSkillWalkLeft";
            }
            else if (animationName == "WaitRight")
            {
                animationName = "UtyuSkillWaitRight";
            }
            else if (animationName == "WaitLeft")
            {
                animationName = "UtyuSkillWaitLeft";
            }

            int stateHash = Animator.StringToHash(animationName);

            if (!animator.HasState(0, stateHash))
            {
                Debug.LogWarning("Animatorにステートがありません: " + animationName);
                animationName = originalAnimationName;
            }
        }

        CurrentAnim = animationName;
        animator.Play(animationName);
    }

}*/
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
    [SerializeField] ActionModeChanger actionModeChanger;

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

    // 走るまでの時間
    float walkTime;
    bool isRunning;

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
        ActionModeChanger.ActionModeChangeEvent += GetActionModeChange;
    }

    void OnDisable()
    {
        InputManager.Instance.actions.Player.Move.performed -= OnMove;
        InputManager.Instance.actions.Player.Move.canceled -= OnMove;
        InputManager.Instance.actions.Player.Jump.performed -= OnJump;
        ActionModeChanger.ActionModeChangeEvent -= GetActionModeChange;
    }

    void FixedUpdate()
    {
        UpdateGrounding();
        UpdateAirState();//空中アニメを反映
        UpdateRunState();
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

    //Event
    void GetActionModeChange(ActionModeChanger.ActionModeType a, ActionModeChanger.ActionModeType b)
    {
        UpdateDirection(true);
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
            UpdateDirection(true);
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
            UpdateDirection(true);
        }
        previousVelocityY = rb.linearVelocityY;
    }

    void UpdateRunState()
    {
        // Neutralのときだけ走る
        if (actionModeChanger.ActionMode != ActionModeChanger.ActionModeType.Neutral)
        {
            if (isRunning)
            {
                isRunning = false;
                UpdateDirection(true);
            }

            walkTime = 0f;
            return;
        }

        // 地上で移動入力がある間だけ時間を進める
        if (IsGrounding && Mathf.Abs(inputX) > 0)
        {
            // 目の前に壁がある間は走るための時間を進めない
            if (IsWallAhead(inputX))
            {
                if (isRunning)
                {
                    isRunning = false;
                    UpdateDirection(true);
                }

                walkTime = 0f;
                return;
            }

            walkTime += Time.fixedDeltaTime;

            // 1秒歩き続けたら走る
            if (!isRunning && walkTime >= 1f)
            {
                isRunning = true;
                UpdateDirection(true);
            }

            // 走っている途中で目の前に壁ができたら走りを解除する
            if (isRunning && IsWallAhead(inputX))
            {
                isRunning = false;
                UpdateDirection(true);
            }
        }
        else
        {
            // 入力を離したら走行時間をリセット
            if (isRunning)
            {
                isRunning = false;
                UpdateDirection(true);
            }

            walkTime = 0f;
        }
    }

    void ApplyMovement()
    {
        float groundVelocityX = 0f;

        if (IsGrounding && CurrentGroundCollider != null)
        {
            IVelocityProvider provider =
                CurrentGroundCollider.transform.parent.GetComponentInChildren<IVelocityProvider>();

            if (provider != null)
            {
                groundVelocityX = provider.Velocity.x;
            }
        }

        float moveMultiplier = IsGrounding ? 1f : 0.7f;

        // 走っている間は移動速度を1.4倍にする
        if (isRunning && IsGrounding)
        {
            moveMultiplier = 1.4f;
        }

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
    void UpdateDirection(bool force = false)//アニメを強制的に変える場合true
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

        // 向き・入力状態が変わっていなければ何もしない(forceならする)
        if (!force && newDirection == lastDirection)
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

        // 地上でDirection.None以外なら歩きまたは走り
        if (isRunning)
        {
            switch (CurrentDirection)
            {
                case Direction.Right:
                    PlayAnimation("RunRight");
                    break;

                case Direction.Left:
                    PlayAnimation("RunLeft");
                    break;
            }
        }
        else
        {
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
        string originalAnimationName = animationName;

        if (actionModeChanger.ActionMode == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            if (animationName == "WalkRight")
            {
                animationName = "UtyuSkillWalkRight";
            }
            else if (animationName == "WalkLeft")
            {
                animationName = "UtyuSkillWalkLeft";
            }
            else if (animationName == "WaitRight")
            {
                animationName = "UtyuSkillWaitRight";
            }
            else if (animationName == "WaitLeft")
            {
                animationName = "UtyuSkillWaitLeft";
            }

            int stateHash = Animator.StringToHash(animationName);

            if (!animator.HasState(0, stateHash))
            {
                Debug.LogWarning("Animatorにステートがありません: " + animationName);
                animationName = originalAnimationName;
            }
        }

        CurrentAnim = animationName;
        animator.Play(animationName);
    }
    bool IsWallAhead(float dir)
    {
        Bounds bounds = myCol.bounds;

        float x = dir > 0
            ? bounds.max.x
            : bounds.min.x;

        float y = bounds.min.y + bounds.size.y * 0.25f;

        Vector2 origin = new Vector2(x, y);
        Vector2 direction = new Vector2(dir, 0);

        int layerMask = ~LayerMask.GetMask("Player");

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction,
            0.4f,
            layerMask);

        Debug.DrawRay(
            origin,
            direction * 0.4f,
            Color.blue);

        return hit.collider != null &&
               hit.collider.attachedRigidbody != null;
    }
}