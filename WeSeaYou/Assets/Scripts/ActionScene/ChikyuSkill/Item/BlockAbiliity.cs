using UnityEngine;

public abstract class BlockAbility : ScriptableObject
{
    public abstract void OnStart(Block block);
    public abstract void OnUpdate(Block block);
}