using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAbility/BombAbility")]
public class BombAbility : BlockAbility
{
    public float explodeAfterSeconds = 10f;

    // 衝突判定用
    public float minSpeedForCollision = 2f;   // この速度以上でないと爆発しない
    public float speedDropThreshold = 3f;     // この速度差を超えたら爆発

    public float checkInterval = 0.1f;

    class State
    {
        public float timer;
        public float checkTimer;
        public float prevSampleSpeed;
        public bool exploded;
    }

    Dictionary<BlockAbilityExcuter, State> states = new Dictionary<BlockAbilityExcuter, State>();

    public override void OnStart(BlockAbilityExcuter block)
    {
        State state = new State();

        Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            state.prevSampleSpeed = rb.linearVelocity.magnitude;
        }

        states[block] = state;

        Debug.Log("Bomb Start");
    }

    public override void OnUpdate(BlockAbilityExcuter block)
    {
        if (!states.TryGetValue(block, out State state)) return;
        if (state.exploded) return;

        // 時間での爆発
        state.timer += Time.deltaTime;
        if (state.timer >= explodeAfterSeconds)
        {
            Explode(block, state);
            return;
        }

        Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // 0.1秒ごとのチェック
        state.checkTimer += Time.deltaTime;
        if (state.checkTimer < checkInterval) return;
        state.checkTimer = 0f;

        float currentSpeed = rb.velocity.magnitude;

        // 絶対速度が一定以上の場合のみ速度差をチェック
        if (currentSpeed >= minSpeedForCollision)
        {
            float speedDrop = state.prevSampleSpeed - currentSpeed;
            if (speedDrop >= speedDropThreshold)
            {
                Explode(block, state);
                return;
            }
        }

        state.prevSampleSpeed = currentSpeed;
    }

    void Explode(BlockAbilityExcuter block, State state)
    {
        state.exploded = true;

        // 爆発エフェクトなどあればここで

        GameObject.Destroy(block.gameObject);
        states.Remove(block);
    }
}