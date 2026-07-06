using UnityEngine;
[CreateAssetMenu(menuName = "BlockAbility/OperableAbility")]
public class OperableAbility : BlockAbility, IVelocityProvider
{
    public Vector2 Velocity { get; private set; }

    public override void OnStart(BlockAbilityExcutor block)
    {
        Debug.Log("Propeller");
        _lastPosition = block.transform.position;
    }
    public override void OnUpdate(BlockAbilityExcutor block)
    {
        Velocity = (block.transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = block.transform.position;
    }
    Vector3 _lastPosition;
    void Awake()
    {
        Debug.Log("Operable");
    }
}