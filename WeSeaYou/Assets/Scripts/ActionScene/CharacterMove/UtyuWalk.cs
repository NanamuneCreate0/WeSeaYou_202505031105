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
        myCol = GetComponent<Collider2D>();
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
}
