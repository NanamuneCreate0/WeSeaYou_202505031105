using UnityEngine;

public class Lift : MonoBehaviour
{
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

    private Vector3 _startPosition;
    private Vector3 _axis;
    private int _direction = 1;

    private void Awake()
    {
        _startPosition = transform.position;

        _axis = moveAxis == MoveAxis.Vertical
            ? Vector3.up
            : Vector3.right;
    }

    private void Update()
    {
        if (!isPowered)
            return;

        float currentDistance = moveAxis == MoveAxis.Vertical
            ? transform.position.y - _startPosition.y
            : transform.position.x - _startPosition.x;

        transform.position += _axis * (_direction * speed * Time.deltaTime);

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
    /*

    public void ToggleActive()
    {
        active = !active;
    }*/
}