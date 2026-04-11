using UnityEngine;

public abstract class BlockAbility : ScriptableObject
{
    public abstract void OnStart(BlockAbilityExcuter block);
    public abstract void OnUpdate(BlockAbilityExcuter block);
}