/*using System.Collections.Generic;座標追う形式
using UnityEngine;

public class UtyuWalk : MonoBehaviour
{
    const float FOLLOW_DELAY = 0.5f;
    const float STOP_FOLLOW_DISTANCE = 3.0f;

    [SerializeField] GameObject Chikyu;

    Rigidbody2D rb;
    Animator animator;
    ChikyuWalk chikyuWalk;

    List<PositionRecord> positionHistory = new List<PositionRecord>();

    string currentAnim;

    struct PositionRecord
    {
        public float time;
        public Vector2 position;
        public string animation;

        public PositionRecord(float time, Vector2 position, string animation)
        {
            this.time = time;
            this.position = position;
            this.animation = animation;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponentInChildren<Animator>();
        chikyuWalk = Chikyu.GetComponent<ChikyuWalk>();
    }

    void Update()
    {
        float currentTime = Time.time;

        positionHistory.Add(
            new PositionRecord(
                currentTime,
                Chikyu.transform.position,
                chikyuWalk.CurrentAnim
            )
        );

        // 0.5秒より古い履歴を削除
        float oldestTime = currentTime - FOLLOW_DELAY;

        while (positionHistory.Count > 0 &&
               positionHistory[0].time < oldestTime)
        {
            positionHistory.RemoveAt(0);
        }
    }

    void FixedUpdate()
    {
        float targetTime = Time.time - FOLLOW_DELAY;

        for (int i = 0; i < positionHistory.Count; i++)
        {
            if (positionHistory[i].time >= targetTime)
            {
                PositionRecord record = positionHistory[i];

                rb.MovePosition(record.position);

                if (currentAnim != record.animation)
                {
                    currentAnim = record.animation;
                    animator.Play(currentAnim);
                }

                break;
            }
        }
    }
}*/

/*アニメがまだそこそこ
using System.Collections.Generic;
using UnityEngine;

public class UtyuWalk : MonoBehaviour//////Chikyuと書いてるけど、実際にはどっちのキャラか分からない　操作しない方
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] Transform player;


    const float stopDist = 0.5f;
    const float jumpDist = 1f;
    const float slowDist = 2f;

    Rigidbody2D rb;
    Collider2D myCol;
    ChikyuWalk playerWalk;

    // 履歴保存用
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
        playerWalk = player.GetComponent<ChikyuWalk>();
    }

    void Update()
    {
        RecordPlayerGround();
        MoveToTarget();
    }

    void RecordPlayerGround()
    {
        recordTimer += Time.deltaTime;

        if (recordTimer < RECORD_INTERVAL) return;
        recordTimer = 0f;



        if (playerWalk != null && playerWalk.IsGrounding)
        {
                GroundRecord record = new GroundRecord();
                record.collider = playerWalk.CurrentGroundCollider;
            record.localPos = record.collider.transform.InverseTransformPoint(player.position);

                history.Enqueue(record);
            
        }
        else
        {
            //空中にいる場合下にRay
            int layerMask = ~LayerMask.GetMask("Player");
            RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, 7f, layerMask);

            if (hit.collider != null)
            {
                GroundRecord record = new GroundRecord();
                record.collider = hit.collider;
                record.localPos = hit.collider.transform.InverseTransformPoint(hit.point);

                history.Enqueue(record);
            }
        }


        // 古いデータ削除（0.5秒分だけ残す）
        int maxCount = Mathf.CeilToInt(DELAY / RECORD_INTERVAL);
        while (history.Count > maxCount)
        {
            history.Dequeue();
        }
    }

    void MoveToTarget()
    {
        if (history.Count == 0) return;

        GroundRecord targetRecord = history.Peek();

        if (targetRecord.collider == null) return;

        Vector2 targetPos = targetRecord.collider.transform.TransformPoint(targetRecord.localPos);


        // 横移動
        //float dir = Mathf.Sign(targetPos.x - transform.position.x);
        //rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        float diff = targetPos.x - transform.position.x;
        float absX = Mathf.Abs(diff);
        // 閾値
        float speed = 0f;
        if (absX > slowDist)
        {
            // 遠い：等速
            speed = moveSpeed;
        }
        else if (absX > stopDist)
        {
            // 中間：減速（線形）
            float t = (absX - stopDist) / (slowDist - stopDist); // 0～1
            speed = moveSpeed * t;
        }
        else
        {
            // 近い：停止
            speed = 0f;
        }

        float dir = Mathf.Sign(diff);
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);


        // 壁判定
        if (IsWallAhead(dir) && absX > jumpDist)
        {
            TryJump();
        }

        // 遠すぎたらワープ
        float warpDist = 10f; // 10以上ならワープ
        float abs = Vector2.Distance(transform.position, player.position);
        if (abs >= warpDist)
        {
            Vector2 warpOffset = new Vector2(-0.2f, 0.5f); // 左0.2、上0.5
            transform.position = (Vector2)targetPos + warpOffset;

            // 履歴を全消し
            history.Clear();
        }
    }
    bool IsWallAhead(float dir)
    {
        Bounds bounds = myCol.bounds;
        float x = dir > 0 ? bounds.max.x : bounds.min.x;
        float y = bounds.min.y + bounds.size.y * 0.25f;
        Vector2 origin = new Vector2(x, y);
        Vector2 direction = new Vector2(dir, 0);

        // Player以外に当たる
        int layerMask = ~LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 0.4f, layerMask);

        // デバッグ
        Debug.DrawRay(origin, direction * 0.4f, Color.blue);
        if (hit.collider != null)
        {
            Debug.Log("WallHit: " + hit.collider.name);
        }
        return (hit.collider != null && hit.collider.attachedRigidbody != null);
    }

    void TryJump()
    {
        bool isGrounded = GroundUtil.CheckGrounded(
            myCol,
            out Collider2D col,
            out Vector2 point);
        if (isGrounded && Time.time - lastJumpTime >= 0.2f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            lastJumpTime = Time.time;
        }
    }
}*/
/*着地追加やろうとした
using System.Collections.Generic;
using UnityEngine;

public class UtyuWalk : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] Transform player;

    const float stopDist = 0.5f;
    const float jumpDist = 1f;
    const float slowDist = 2f;

    Rigidbody2D rb;
    Collider2D myCol;
    ChikyuWalk playerWalk;
    Animator animator;

    // 向き
    public enum Direction
    {
        Left = -1,
        None = 0,
        Right = 1
    }

    public Direction CurrentDirection { get; private set; } = Direction.Right;
    Direction? lastDirection = null;

    // 現在のアニメーション
    public string CurrentAnim { get; private set; }

    // 履歴保存用
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

    bool isGrounding;
    bool wasGrounding;
    bool groundingInitialized;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCol = GetComponentInChildren<Collider2D>();
        playerWalk = player.GetComponent<ChikyuWalk>();
        animator = GetComponentInChildren<Animator>();

        PlayAnimation("UtyuWaitRight");
    }

    void Update()
    {
        RecordPlayerGround();
        MoveToTarget();
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
            // 空中にいる場合は下にRay
            int layerMask = ~LayerMask.GetMask("Player");

            RaycastHit2D hit = Physics2D.Raycast(
                player.position,
                Vector2.down,
                7f,
                layerMask
            );

            if (hit.collider != null)
            {
                GroundRecord record = new GroundRecord();

                record.collider = hit.collider;
                record.localPos =
                    hit.collider.transform.InverseTransformPoint(hit.point);

                history.Enqueue(record);
            }
        }

        // 古いデータ削除
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
                targetRecord.localPos
            );

        // 横移動
        float diff = targetPos.x - transform.position.x;
        float absX = Mathf.Abs(diff);

        float speed = 0f;

        if (absX > slowDist)
        {
            // 遠い：等速
            speed = moveSpeed;
        }
        else if (absX > stopDist)
        {
            // 中間：減速
            float t =
                (absX - stopDist) /
                (slowDist - stopDist);

            speed = moveSpeed * t;
        }
        else
        {
            // 近い：停止
            speed = 0f;
        }

        float dir = Mathf.Sign(diff);

        rb.linearVelocity =
            new Vector2(
                dir * speed,
                rb.linearVelocity.y
            );

        // アニメーション更新
        UpdateDirection(dir);

        // 壁判定
        if (IsWallAhead(dir) && absX > jumpDist)
        {
            TryJump();
        }

        // 遠すぎたらワープ
        float warpDist = 10f;

        float abs =
            Vector2.Distance(
                transform.position,
                player.position
            );

        if (abs >= warpDist)
        {
            Vector2 warpOffset =
                new Vector2(-0.2f, 0.5f);

            transform.position =
                (Vector2)targetPos + warpOffset;

            history.Clear();
        }
    }
void UpdateGrounding()
    {
        isGrounding = GroundUtil.CheckGrounded(
            myCol,
            out Collider2D col,
            out Vector2 point);

        if (isGrounding)
        {
            myCol = col;
        }
        else
        {
            myCol = null;
        }
        //着地アニメ

        // 初回は着地扱いにしない
        if (!groundingInitialized)
        {
            wasGrounding = isGrounding;
            groundingInitialized = true;
            return;
        }

        // 空中 → 接地になった瞬間
        if (!wasGrounding && isGrounding)
        {
            lastDirection = null;
            UpdateDirection();
        }
        wasGrounding = IsGrounding;
    }

    // 向き・アニメーションの変更
    void UpdateDirection(float dir)
    {
        Direction newDirection = Direction.None;

        if (dir > 0)
        {
            newDirection = Direction.Right;
        }
        else if (dir < 0)
        {
            newDirection = Direction.Left;
        }

        // 向きが変わっていなければ何もしない
        if (newDirection == lastDirection)
        {
            // 空中ならジャンプ・落下アニメーションの更新だけする
            if (!IsGrounding())
            {
                PlayJumpAnimation();
            }

            return;
        }

        lastDirection = newDirection;

        // None以外なら向きを更新
        if (newDirection != Direction.None)
        {
            CurrentDirection = newDirection;
        }

        // 空中ならジャンプ・落下アニメーション
        if (!IsGrounding())
        {
            PlayJumpAnimation();
            return;
        }

        // 地上で止まっているなら待機
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

        // 地上で移動中
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
        if (CurrentAnim == animationName)
        {
            return;
        }

        CurrentAnim = animationName;
        animator.Play(animationName);
    }

    bool IsGrounding()
    {
        return GroundUtil.CheckGrounded(
            myCol,
            out Collider2D groundCol,
            out Vector2 groundPoint
        );
    }

    bool IsWallAhead(float dir)
    {
        Bounds bounds = myCol.bounds;

        float x =
            dir > 0
                ? bounds.max.x
                : bounds.min.x;

        float y =
            bounds.min.y +
            bounds.size.y * 0.25f;

        Vector2 origin =
            new Vector2(x, y);

        Vector2 direction =
            new Vector2(dir, 0);

        // Player以外に当たる
        int layerMask =
            ~LayerMask.GetMask("Player");

        RaycastHit2D hit =
            Physics2D.Raycast(
                origin,
                direction,
                0.4f,
                layerMask
            );

        // デバッグ
        Debug.DrawRay(
            origin,
            direction * 0.4f,
            Color.blue
        );

        return (
            hit.collider != null &&
            hit.collider.attachedRigidbody != null
        );
    }

    void TryJump()
    {
        bool isGrounded =
            GroundUtil.CheckGrounded(
                myCol,
                out Collider2D col,
                out Vector2 point
            );

        if (
            isGrounded &&
            Time.time - lastJumpTime >= 0.2f
        )
        {
            rb.linearVelocity =
                new Vector2(
                    rb.linearVelocity.x,
                    jumpPower
                );

            lastJumpTime = Time.time;
        }
    }
}*/
using System.Collections.Generic;
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
}