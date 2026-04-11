using System.Collections.Generic;
using UnityEngine;

public class BlockAbilityExcuter : MonoBehaviour
{
    public List<BlockAbility> BlockAbilities = new List<BlockAbility>();

    private void Start()
    {
        foreach (var ability in BlockAbilities)
        {
            ability?.OnStart(this);
        }
    }

    private void Update()
    {
        foreach (var ability in BlockAbilities)
        {
            ability?.OnUpdate(this);
        }
    }
}