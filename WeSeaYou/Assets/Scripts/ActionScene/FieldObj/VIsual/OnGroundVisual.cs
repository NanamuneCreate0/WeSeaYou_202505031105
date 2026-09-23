using System.Collections;
using UnityEngine;

public class OnGroundVisual : MonoBehaviour
{
    [SerializeField] ParticleSystem groundParticlePrefab;

    [SerializeField] GameObject dustLeft;
    [SerializeField] GameObject dustRight;

    private Collider2D col;
    private Rigidbody2D rb;

    private bool isGrounding = true;//èâä˙ãÛíÜÇ≈Ç‡trueÅ®falseÇ…Ç»ÇÈÇÃÇ≈ñ‚ëËÇ»Ç¢
    private bool lastIsGrounding = true;

    void Start()
    {
        col = GetComponentInChildren<Collider2D>();
        rb = col.GetComponentInParent<Rigidbody2D>();
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
        if (rb != null && rb.mass >= 7f)
        {
            StartDustAnim();
        }

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
    }

    void StartDustAnim()
    {
        StartCoroutine(StartDustAnimCoroutine());
    }
    IEnumerator StartDustAnimCoroutine()
    {
        yield return new WaitForSeconds(0.063f);

        float bottomY = col.bounds.min.y - 0.1f;

        if (dustLeft != null)
        {
            GameObject left = Instantiate(dustLeft);
            left.transform.position = new Vector2(
                col.bounds.min.x - 0.32f,
                bottomY
            );
            Destroy(left, 0.7f);
        }

        if (dustRight != null)
        {
            GameObject right = Instantiate(dustRight);
            right.transform.position = new Vector2(
                col.bounds.max.x + 0.32f,
                bottomY
            );
            Destroy(right, 0.7f);
        }
    }
}