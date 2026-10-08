/*using System.Collections.Generic;
using UnityEngine;

public class UtyuWalk : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] Transform player;

    const float stopDist = 1f;//停止する距離
    const float jumpDist = 1.5f;//ジャンプ開始の距離
    const float slowDist = 2.5f;//減速を開始する距離
    const float waitSpeed = 0.5f;//Waitアニメーションに切り替える速度の境界

    Rigidbody2D rb;
    Collider2D myCol;
    Animator animator;
    ChikyuWalk playerWalk;

    public bool IsGrounding { get; private set; }

    bool wasGrounding;
    bool groundingInitialized;

    public enum Direction
    {
        Left = -1,
        None = 0,
        Right = 1
    }

    public Direction CurrentDirection { get; private set; } = Direction.Right;
    Direction lastDirection = Direction.None;

    public string CurrentAnim { get; private set; }

    class GroundRecord
    {
        public Collider2D collider;
        public Vector2 localPos;
    }

    Queue<GroundRecord> history = new Queue<GroundRecord>();

    float recordTimer = 0f;
    float lastJumpTime = -999f;

    const float RECORD_INTERVAL = 0.1f;
    const float DELAY = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCol = GetComponentInChildren<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        playerWalk = player.GetComponent<ChikyuWalk>();

        PlayAnimation("WaitRight");
    }

    void Update()
    {
        UpdateGrounding();
        RecordPlayerGround();
        MoveToTarget();
    }

    void UpdateGrounding()
    {
        IsGrounding = GroundUtil.CheckGrounded(
            myCol,
            out Collider2D col,
            out Vector2 point);

        if (!groundingInitialized)
        {
            wasGrounding = IsGrounding;
            groundingInitialized = true;
            return;
        }

        if (!wasGrounding && IsGrounding)
        {
            lastDirection = Direction.None;
            UpdateDirection(0f);
        }

        wasGrounding = IsGrounding;
    }

    void RecordPlayerGround()
    {
        recordTimer += Time.deltaTime;

        if (recordTimer < RECORD_INTERVAL)
        {
            return;
        }

        recordTimer = 0f;

        if (playerWalk != null && playerWalk.IsGrounding)
        {
            GroundRecord record = new GroundRecord();

            record.collider = playerWalk.CurrentGroundCollider;
            record.localPos =
                record.collider.transform.InverseTransformPoint(player.position);

            history.Enqueue(record);
        }
        else
        {
            int layerMask = ~LayerMask.GetMask("Player");

            RaycastHit2D hit = Physics2D.Raycast(
                player.position,
                Vector2.down,
                7f,
                layerMask);

            if (hit.collider != null)
            {
                GroundRecord record = new GroundRecord();

                record.collider = hit.collider;
                record.localPos =
                    hit.collider.transform.InverseTransformPoint(hit.point);

                history.Enqueue(record);
            }
        }

        int maxCount = Mathf.CeilToInt(DELAY / RECORD_INTERVAL);

        while (history.Count > maxCount)
        {
            history.Dequeue();
        }
    }

    void MoveToTarget()
    {
        if (history.Count == 0)
        {
            return;
        }

        GroundRecord targetRecord = history.Peek();

        if (targetRecord.collider == null)
        {
            return;
        }

        Vector2 targetPos =
            targetRecord.collider.transform.TransformPoint(
                targetRecord.localPos);

        float diff = targetPos.x - transform.position.x;
        float absX = Mathf.Abs(diff);

        float speed = 0f;

        if (absX > slowDist)
        {
            speed = moveSpeed;
        }
        else if (absX > stopDist)
        {
            float t = (absX - stopDist) / (slowDist - stopDist);
            speed = moveSpeed * t;
        }
        else
        {
            speed = 0f;
        }

        float dir = Mathf.Sign(diff);

        rb.linearVelocity = new Vector2(
            dir * speed,
            rb.linearVelocity.y);

        UpdateDirection(dir);

        if (IsWallAhead(dir) && absX > jumpDist)
        {
            TryJump();
        }

        float warpDist = 10f;
        float abs = Vector2.Distance(
            transform.position,
            player.position);

        if (abs >= warpDist)
        {
            Vector2 warpOffset = new Vector2(-0.2f, 0.5f);

            transform.position =
                (Vector2)targetPos + warpOffset;

            history.Clear();
        }
    }

    void UpdateDirection(float dir)
    {
        Direction newDirection = Direction.None;

        if (Mathf.Abs(rb.linearVelocityX) > waitSpeed)
        {
            if (dir > 0)
            {
                newDirection = Direction.Right;
            }
            else if (dir < 0)
            {
                newDirection = Direction.Left;
            }
        }

        if (newDirection == lastDirection)
        {
            if (!IsGrounding)
            {
                PlayJumpAnimation();
            }

            return;
        }

        lastDirection = newDirection;

        if (newDirection != Direction.None)
        {
            CurrentDirection = newDirection;
        }

        if (!IsGrounding)
        {
            PlayJumpAnimation();
            return;
        }

        if (newDirection == Direction.None)
        {
            if (CurrentDirection == Direction.Right)
            {
                PlayAnimation("WaitRight");
            }
            else
            {
                PlayAnimation("WaitLeft");
            }

            return;
        }

        if (newDirection == Direction.Right)
        {
            PlayAnimation("WalkRight");
        }
        else
        {
            PlayAnimation("WalkLeft");
        }
    }

    void PlayJumpAnimation()
    {
        if (rb.linearVelocityY >= 0)
        {
            if (CurrentDirection == Direction.Right)
            {
                PlayAnimation("JumpRight");
            }
            else
            {
                PlayAnimation("JumpLeft");
            }
        }
        else
        {
            if (CurrentDirection == Direction.Right)
            {
                PlayAnimation("FallRight");
            }
            else
            {
                PlayAnimation("FallLeft");
            }
        }
    }

    void PlayAnimation(string animationName)
    {
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

    void TryJump()
    {
        bool isGrounded = GroundUtil.CheckGrounded(
            myCol,
            out Collider2D col,
            out Vector2 point);

        if (isGrounded &&
            Time.time - lastJumpTime >= 0.2f)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpPower);

            lastJumpTime = Time.time;
        }
    }
}*/
using System.Collections.Generic;
using UnityEngine;

public class UtyuWalk : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] Transform player;
    const float stopDist = 1f;//停止する距離
    const float jumpDist = 1.5f;//ジャンプ開始の距離
    const float slowDist = 1.7f;//減速を開始する距離
    const float RUN_DIST = 2.1f;//走り始める距離
    const float RUN_END_DIST = 1.7f;//走り終わる距離
    const float waitSpeed = 0.5f;//Waitアニメーションに切り替える速度の境界

    Rigidbody2D rb;
    Collider2D myCol;
    Animator animator;
    ChikyuWalk playerWalk;

    public bool IsGrounding { get; private set; }

    bool wasGrounding;
    bool groundingInitialized; 
    bool isRunning;

    public enum Direction
    {
        Left = -1,
        None = 0,
        Right = 1
    }

    public Direction CurrentDirection { get; private set; } = Direction.Right;
    Direction lastDirection = Direction.None;

    public string CurrentAnim { get; private set; }

    class GroundRecord
    {
        public Collider2D collider;
        public Vector2 localPos;
    }

    Queue<GroundRecord> history = new Queue<GroundRecord>();

    float recordTimer = 0f;
    float lastJumpTime = -999f;

    const float RECORD_INTERVAL = 0.1f;
    const float DELAY = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCol = GetComponentInChildren<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        playerWalk = player.GetComponent<ChikyuWalk>();

        PlayAnimation("WaitRight");
    }

    void Update()
    {
        UpdateGrounding();
        RecordPlayerGround();
        MoveToTarget();
    }

    void UpdateGrounding()
    {
        IsGrounding = GroundUtil.CheckGrounded(
            myCol,
            out Collider2D col,
            out Vector2 point);

        if (!groundingInitialized)
        {
            wasGrounding = IsGrounding;
            groundingInitialized = true;
            return;
        }

        if (!wasGrounding && IsGrounding)
        {
            lastDirection = Direction.None;
            UpdateDirection(0f, false);
        }

        wasGrounding = IsGrounding;
    }

    void RecordPlayerGround()
    {
        recordTimer += Time.deltaTime;

        if (recordTimer < RECORD_INTERVAL)
        {
            return;
        }

        recordTimer = 0f;

        if (playerWalk != null && playerWalk.IsGrounding)
        {
            GroundRecord record = new GroundRecord();

            record.collider = playerWalk.CurrentGroundCollider;
            record.localPos =
                record.collider.transform.InverseTransformPoint(player.position);

            history.Enqueue(record);
        }
        else
        {
            int layerMask = ~LayerMask.GetMask("Player");

            RaycastHit2D hit = Physics2D.Raycast(
                player.position,
                Vector2.down,
                7f,
                layerMask);

            if (hit.collider != null)
            {
                GroundRecord record = new GroundRecord();

                record.collider = hit.collider;
                record.localPos =
                    hit.collider.transform.InverseTransformPoint(hit.point);

                history.Enqueue(record);
            }
        }

        int maxCount = Mathf.CeilToInt(DELAY / RECORD_INTERVAL);

        while (history.Count > maxCount)
        {
            history.Dequeue();
        }
    }

    void MoveToTarget()
    {
        if (history.Count == 0)
        {
            return;
        }

        GroundRecord targetRecord = history.Peek();

        if (targetRecord.collider == null)
        {
            return;
        }

        Vector2 targetPos =
            targetRecord.collider.transform.TransformPoint(
                targetRecord.localPos);

        //diff,absX,speed,dirが距離、距離の絶対値、スピード、方向
        float diff = targetPos.x - transform.position.x;
        float absX = Mathf.Abs(diff);

        float speed = 0f;

        if (absX >= RUN_DIST)
        {
            speed = runSpeed;
        }
        else if (absX > slowDist)
        {
            speed = moveSpeed;
        }
        else if (absX > stopDist)
        {
            float t = (absX - stopDist) / (slowDist - stopDist);
            speed = moveSpeed * t;
        }
        else
        {
            speed = 0f;
        }

        float dir = Mathf.Sign(diff);

        rb.linearVelocity = new Vector2(
            dir * speed,
            rb.linearVelocity.y);

        if (!isRunning && absX >= RUN_DIST && IsGrounding)
        {
            isRunning = true;
        }
        else if (isRunning && absX < RUN_END_DIST)
        {
            isRunning = false;
        }
        UpdateDirection(dir, isRunning);

        if (IsWallAhead(dir) && absX > jumpDist)
        {
            TryJump();
        }

        float warpDist = 10f;
        float abs = Vector2.Distance(
            transform.position,
            player.position);

        if (abs >= warpDist)
        {
            Vector2 warpOffset = new Vector2(-0.2f, 0.5f);

            transform.position =
                (Vector2)targetPos + warpOffset;

            history.Clear();
        }
    }

    void UpdateDirection(float dir, bool isRunning)
    {
        Direction newDirection = Direction.None;

        if (Mathf.Abs(rb.linearVelocityX) > waitSpeed)
        {
            if (dir > 0)
            {
                newDirection = Direction.Right;
            }
            else if (dir < 0)
            {
                newDirection = Direction.Left;
            }
        }

        if (newDirection == lastDirection)
        {
            if (!IsGrounding)
            {
                PlayJumpAnimation();
                return;
            }

            if (newDirection == Direction.None)
            {
                if (CurrentDirection == Direction.Right)
                {
                    PlayAnimation("WaitRight");
                }
                else
                {
                    PlayAnimation("WaitLeft");
                }

                return;
            }

            if (isRunning)
            {
                if (newDirection == Direction.Right)
                {
                    PlayAnimation("RunRight");
                }
                else
                {
                    PlayAnimation("RunLeft");
                }
            }
            else
            {
                if (newDirection == Direction.Right)
                {
                    PlayAnimation("WalkRight");
                }
                else
                {
                    PlayAnimation("WalkLeft");
                }
            }

            return;
        }

        lastDirection = newDirection;

        if (newDirection != Direction.None)
        {
            CurrentDirection = newDirection;
        }

        if (!IsGrounding)
        {
            PlayJumpAnimation();
            return;
        }

        if (newDirection == Direction.None)
        {
            if (CurrentDirection == Direction.Right)
            {
                PlayAnimation("WaitRight");
            }
            else
            {
                PlayAnimation("WaitLeft");
            }

            return;
        }

        if (isRunning)
        {
            if (newDirection == Direction.Right)
            {
                PlayAnimation("RunRight");
            }
            else
            {
                PlayAnimation("RunLeft");
            }

            return;
        }

        if (newDirection == Direction.Right)
        {
            PlayAnimation("WalkRight");
        }
        else
        {
            PlayAnimation("WalkLeft");
        }
    }

    void PlayJumpAnimation()
    {
        if (rb.linearVelocityY >= 0)
        {
            if (CurrentDirection == Direction.Right)
            {
                PlayAnimation("JumpRight");
            }
            else
            {
                PlayAnimation("JumpLeft");
            }
        }
        else
        {
            if (CurrentDirection == Direction.Right)
            {
                PlayAnimation("FallRight");
            }
            else
            {
                PlayAnimation("FallLeft");
            }
        }
    }

    void PlayAnimation(string animationName)
    {
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

    void TryJump()
    {
        bool isGrounded = GroundUtil.CheckGrounded(
            myCol,
            out Collider2D col,
            out Vector2 point);

        if (isGrounded &&
            Time.time - lastJumpTime >= 0.2f)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpPower);

            lastJumpTime = Time.time;
        }
    }
}