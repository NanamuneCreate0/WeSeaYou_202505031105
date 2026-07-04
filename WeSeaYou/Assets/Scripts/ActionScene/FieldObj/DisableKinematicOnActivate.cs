using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DisableKinematicOnActivate : MonoBehaviour, IActivatable
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Activate()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }
}