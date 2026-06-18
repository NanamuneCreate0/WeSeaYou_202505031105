using System.Collections.Generic;
using UnityEngine;

public class BlockAbilityExcuter : MonoBehaviour
{
    public List<BlockAbility> BlockAbilities = new();
    private readonly List<BlockAbility> runtimeAbilities = new();
    private bool isDestroyed;


    //ŒÂ•ÊPrefabÝ’è‚ª‚ß‚ñ‚Ç‚­‚³‚¢‚½‚ßA‚±‚±‚É‚¨‚¢‚Æ‚­B
    [SerializeField] private GameObject bombHitboxPrefab;
    public GameObject BombHitboxPrefab => bombHitboxPrefab;

    private void Start()
    {
        foreach (var ability in BlockAbilities)
        {
            if (ability == null) continue;

            BlockAbility instance = Instantiate(ability);
            runtimeAbilities.Add(instance);

            instance.OnStart(this);
        }
    }
    private void Update()
    {
        foreach (BlockAbility ability in runtimeAbilities)
        {
            ability.OnUpdate(this);
        }
    }
    public void DestroyBlock()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        foreach (var ability in runtimeAbilities)
        {
            ability?.OnBlockDestroy(this);
        }

        Destroy(gameObject);
    }
}