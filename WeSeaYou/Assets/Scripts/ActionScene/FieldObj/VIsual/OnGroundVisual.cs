using UnityEngine;

public class OnGroundVisual : MonoBehaviour
{
    [SerializeField] ParticleSystem groundParticlePrefab;

    [SerializeField] GameObject dustLeft;
    [SerializeField] GameObject dustRight;

    public Collider2D col;

    private bool isGrounding = true;
    private bool lastIsGrounding = true;

    void Start()
    {
        if (col == null)
        {
            col = GetComponentInChildren<Collider2D>();
        }

        UpdateDustPosition();
    }

    void Update()
    {
        isGrounding = GroundUtil.CheckGrounded(
            col,
            out Collider2D col1,
            out Vector2 point);

        if (!lastIsGrounding && isGrounding)
        {
            OnGround();
        }

        lastIsGrounding = isGrounding;
    }

    void OnGround()
    {
        Vector2 spawnPosition = new Vector2(
            col.bounds.center.x,
            col.bounds.min.y - 0.22f
        );

        ParticleSystem particle = Instantiate(
            groundParticlePrefab,
            spawnPosition,
            Quaternion.identity
        );

        particle.Play();

        Destroy(
            particle.gameObject,
            particle.main.duration + particle.main.startLifetime.constantMax
        );

        UpdateDustPosition();
    }

    void UpdateDustPosition()
    {
        if (col == null)
        {
            return;
        }

        float bottomY = col.bounds.min.y;

        if (dustLeft != null)
        {
            dustLeft.transform.position = new Vector2(
                col.bounds.min.x,
                bottomY
            );
        }

        if (dustRight != null)
        {
            dustRight.transform.position = new Vector2(
                col.bounds.max.x,
                bottomY
            );
        }
    }
}