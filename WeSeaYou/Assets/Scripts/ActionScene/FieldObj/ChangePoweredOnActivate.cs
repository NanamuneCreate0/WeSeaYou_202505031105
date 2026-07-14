using UnityEngine;

public class ChangePoweredOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private Lift targetLift;
    [SerializeField] private bool powered = true;

    public void Activate()
    {
        targetLift.SetPowered(powered);
    }
}