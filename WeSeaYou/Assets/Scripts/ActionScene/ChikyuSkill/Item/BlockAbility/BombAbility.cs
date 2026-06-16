using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BlockAbility/BombAbility")]
public class BombAbility : BlockAbility
{
    public float explodeAfterSeconds = 10f;

    // �Փ˔���p
    public float minSpeedForCollision = 2f;   // ���̑��x�ȏ�łȂ��Ɣ������Ȃ�
    public float speedDropThreshold = 3f;     // ���̑��x���𒴂����甚��

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

        // ���Ԃł̔���
        state.timer += Time.deltaTime;
        if (state.timer >= explodeAfterSeconds)
        {
            Explode(block, state);
            return;
        }

        Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // 0.1�b���Ƃ̃`�F�b�N
        state.checkTimer += Time.deltaTime;
        if (state.checkTimer < checkInterval) return;
        state.checkTimer = 0f;

        float currentSpeed = rb.linearVelocity.magnitude;

        // ��Α��x�����ȏ�̏ꍇ�̂ݑ��x�����`�F�b�N
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

        // �����G�t�F�N�g�Ȃǂ���΂�����

        GameObject.Destroy(block.gameObject);
        states.Remove(block);
    }
}