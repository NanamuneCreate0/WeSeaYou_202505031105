using UnityEngine;
[CreateAssetMenu(menuName = "BlockAbility/OperableAbility")]
public class OperableAbility : BlockAbility
{
    public override void OnStart(BlockAbilityExcuter block)
    {
        Debug.Log("Operable");
    }
    public override void OnUpdate(BlockAbilityExcuter block)
    {
    }
}
