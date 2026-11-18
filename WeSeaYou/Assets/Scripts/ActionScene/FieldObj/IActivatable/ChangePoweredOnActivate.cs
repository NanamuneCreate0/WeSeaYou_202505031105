using UnityEngine;

public class ChangePoweredOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private MonoBehaviour targetLift;
    [SerializeField] private bool powered = true;

    public void Activate()
    {
        if (targetLift is Lift lift)
        {
            lift.SetPowered(powered);
        }
        else if (targetLift is WaypointLift waypointLift)
        {
            waypointLift.SetPowered(powered);
        }
    }
}