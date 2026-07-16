using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PropellerAbility : MonoBehaviour
{
    private const float MAX_FALL_SPEED = -0.5f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _rb.linearVelocity;

        // —Ž‰º’†‚¾‚¯“K—p
        if (velocity.y < MAX_FALL_SPEED)
        {
            velocity.y = MAX_FALL_SPEED;
            _rb.linearVelocity = velocity;
        }
    }
}