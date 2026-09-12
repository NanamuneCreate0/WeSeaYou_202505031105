using UnityEngine;

public class VelocityProviderObj : MonoBehaviour, IVelocityProvider
{
    public Vector2 Velocity { get; private set; }
    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;

    }
    private void FixedUpdate()
    {
        Velocity = (transform.position - _lastPosition) / Time.fixedDeltaTime;
        _lastPosition = transform.position;
    }
}
