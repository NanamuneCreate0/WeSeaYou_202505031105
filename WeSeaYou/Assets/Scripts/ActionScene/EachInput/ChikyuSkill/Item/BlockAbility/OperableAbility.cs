using UnityEngine;
[CreateAssetMenu(menuName = "BlockAbility/OperableAbility")]
public class OperableAbility : BlockAbility
{
    public override void OnStart(BlockAbilityExcutor block)
    {
        Debug.Log("Operable");
    }
    public override void OnUpdate(BlockAbilityExcutor block)
    {
    }
}
