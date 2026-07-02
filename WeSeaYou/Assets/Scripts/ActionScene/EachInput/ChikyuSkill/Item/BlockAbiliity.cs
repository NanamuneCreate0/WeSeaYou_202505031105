using UnityEngine;

public abstract class BlockAbility : ScriptableObject
{
    public abstract void OnStart(BlockAbilityExcutor block);
    public abstract void OnUpdate(BlockAbilityExcutor block);
    public virtual void OnBlockDestroy(BlockAbilityExcutor block) { }
}