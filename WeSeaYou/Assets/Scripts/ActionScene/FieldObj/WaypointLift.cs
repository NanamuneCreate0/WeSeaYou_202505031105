using System.Collections.Generic;
using UnityEngine;

public class WaypointLift : MonoBehaviour, IVelocityProvider, IActivatable
{
    public Vector2 Velocity { get; private set; }

    [Header("State")]
    [SerializeField] private bool isPowered = true;

    [Header("Movement")]
    [SerializeField] private List<Vector2> waypoints = new();
    [SerializeField] private float speed = 1f;

    private Rigidbody2D _rb;
    private Vector2 _startPosition;

    private int _targetIndex = 0;
    private Vector2 _targetPosition;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = _rb.position;
        waypoints.Insert(0, Vector2.zero);
        _targetIndex = waypoints.Count > 1 ? 1 : 0;
        _targetPosition = GetTargetPosition();
    }

    private void FixedUpdate()
    {
        if (!isPowered)
        {
            Velocity = Vector2.zero;
            return;
        }

        Vector2 toTarget = _targetPosition - _rb.position;
        float distance = toTarget.magnitude;
        float moveDistance = speed * Time.fixedDeltaTime;

        if (moveDistance >= distance)
        {
            Velocity = toTarget / Time.fixedDeltaTime;//IVelocityProvider—p
            _rb.MovePosition(_targetPosition);
            AdvanceTarget();
            return;
        }

        Velocity = toTarget.normalized * speed;
        _rb.MovePosition(_rb.position + Velocity * Time.fixedDeltaTime);
        //Debug.Log(Velocity);
    }

    private void AdvanceTarget()
    {
        _targetIndex = (_targetIndex + 1) % waypoints.Count;
        _targetPosition = GetTargetPosition();
    }

    private Vector2 GetTargetPosition()
    {
        return _startPosition + waypoints[_targetIndex];
    }
    public void Activate()
    {
        isPowered = true;
    }
}