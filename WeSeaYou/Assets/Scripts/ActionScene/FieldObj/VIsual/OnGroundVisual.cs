using UnityEngine;

public class OnGroundVisual : MonoBehaviour
{
    [SerializeField] ParticleSystem groundParticlePrefab;
    private Collider2D col;
    private bool isGrounding = true;
    private bool lastIsGrounding = true;

    void Start()
    {
        col = GetComponent<Collider2D>();
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
        Debug.Log("OnG");
        Vector2 spawnPosition = new Vector2(
            col.bounds.center.x,
            col.bounds.min.y
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
    }
}