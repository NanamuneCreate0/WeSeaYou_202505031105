using System.Collections.Generic;
using UnityEngine;

public class UtyuWalk : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;

    [SerializeField] Transform player;

    Rigidbody2D rb;
    IsGroundingJudger isGroundingJudger;

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
        isGroundingJudger = transform.GetChild(0).GetComponent<IsGroundingJudger>();
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

        // プレイヤーの接地情報取得
        var playerGround = player.GetComponentInChildren<IsGroundingJudger>();

        if (playerGround != null && playerGround.IsGrounding)
        {
            Collider2D col = playerGround.CurrentGroundCollider;

            if (col != null)
            {
                GroundRecord record = new GroundRecord();
                record.collider = col;
                record.localPos = col.transform.InverseTransformPoint(player.position);

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
        float abs = Mathf.Abs(diff);
        // 閾値
        float stopDist = 0.5f;
        float slowDist = 2f;
        float speed = 0f;
        if (abs > slowDist)
        {
            // 遠い：等速
            speed = moveSpeed;
        }
        else if (abs > stopDist)
        {
            // 中間：減速（線形）
            float t = (abs - stopDist) / (slowDist - stopDist); // 0～1
            speed = moveSpeed * t*t;
        }
        else
        {
            // 近い：停止
            speed = 0f;
        }

        float dir = Mathf.Sign(diff);
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);


        // 壁判定
        if (IsWallAhead(dir) && abs > stopDist)
        {
            TryJump();
        }

        // 遠すぎたらワープ
        float warpDist = 10f; // 10以上ならワープ
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
        Vector2 origin = (Vector2)transform.position + Vector2.down * 0.5f;
        Vector2 direction = new Vector2(dir, 0);

        int layerMask = LayerMask.GetMask("Ground");

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 0.5f, layerMask);

        return hit.collider != null;
    }

    void TryJump()
    {
        if (isGroundingJudger.IsGrounding && Time.time - lastJumpTime >= 0.2f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            lastJumpTime = Time.time;
        }
    }

    public void IsHandling_Jump()
    {
        TryJump();
    }
}
