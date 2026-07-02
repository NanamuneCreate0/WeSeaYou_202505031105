using UnityEngine;
[CreateAssetMenu(menuName = "BlockAbility/PropellerAbility")]
public class PropellerAbility : BlockAbility
{
    public override void OnStart(BlockAbilityExcutor block)
    {
        Debug.Log("Propeller");
    }
    public override void OnUpdate(BlockAbilityExcutor block)
    {
    }
}
