using UnityEngine;

public class Lift : MonoBehaviour, IVelocityProvider
{
    public Vector2 Velocity { get; private set; }
    private enum MoveAxis
    {
        Vertical,
        Horizontal
    }

    [Header("State")]
    [SerializeField] private bool isPowered = true;

    [Header("Movement")]
    [SerializeField] private MoveAxis moveAxis = MoveAxis.Vertical;
    [SerializeField] private float positiveDistance = 0f;
    [SerializeField] private float negativeDistance = 2f;
    [SerializeField] private float speed = 1f;


    private Rigidbody2D _rb;
    private Vector2 _startPosition;
    private Vector2 _axis;
    private int _direction = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = _rb.position;

        _axis = moveAxis == MoveAxis.Vertical
            ? Vector2.up
            : Vector2.right;
    }

    private void FixedUpdate()
    {
        if (!isPowered)
            return;

        float currentDistance = moveAxis == MoveAxis.Vertical
            ? _rb.position.y - _startPosition.y
            : _rb.position.x - _startPosition.x;

        //transform.position += _axis * (_direction * speed * Time.deltaTime);

        Vector2 velocity = _axis * (_direction * speed);
        Velocity = velocity;

        Vector2 nextPosition = _rb.position + velocity * Time.fixedDeltaTime;
        _rb.MovePosition(nextPosition);

        if (_direction > 0 && currentDistance >= positiveDistance)
        {
            _direction = -1;
        }
        else if (_direction < 0 && currentDistance <= -negativeDistance)
        {
            _direction = 1;
        }
    }
    public void SetPowered(bool active)
    {
        this.isPowered = active;
    }
    public void Activate()
    {
        isPowered = true;
    }
}