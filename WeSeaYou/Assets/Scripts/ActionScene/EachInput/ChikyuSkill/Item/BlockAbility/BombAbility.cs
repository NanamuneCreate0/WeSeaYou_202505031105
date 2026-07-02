using UnityEngine;

[CreateAssetMenu(menuName = "BlockAbility/BombAbility")]
public class BombAbility : BlockAbility
{
    const float explodeAfterSeconds = 4f;

    float timer;

    public override void OnStart(BlockAbilityExcutor block)
    {
        timer = 0f;
    }

    public override void OnUpdate(BlockAbilityExcutor block)
    {
        timer += Time.deltaTime;

        if (timer >= explodeAfterSeconds)
        {
            block.DestroyBlock();
        }
    }
    public override void OnBlockDestroy(BlockAbilityExcutor block)
    {
        Debug.Log("爆発");
        Instantiate(
             block.BombHitboxPrefab,
             block.transform.position,
             Quaternion.identity
         );

    }
}