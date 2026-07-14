using UnityEngine;
public class OperableAbility : MonoBehaviour, IVelocityProvider
{
    public Vector2 Velocity { get; private set; }
    private void Start()
    {
        _lastPosition = transform.position;
    }
    private void Update()
    {
        Velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;
    }
    Vector3 _lastPosition;
}