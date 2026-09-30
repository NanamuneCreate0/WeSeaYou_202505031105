using System.Collections;
using UnityEngine;

public class BombAbility : MonoBehaviour
{
    const float explodeAfterSeconds = 4f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        StartCoroutine(ExplodeCoroutine());
    }

    private IEnumerator ExplodeCoroutine()
    {
        // ”š”­‚Ü‚Å‚Ì2/5‚ÌŽžŠÔ‘Ò‚Â
        yield return new WaitForSeconds(explodeAfterSeconds * 2f / 5f);

        VirtualAnimPlay();

        // Žc‚è‚ÌŽžŠÔ‘Ò‚Â
        yield return new WaitForSeconds(explodeAfterSeconds * 3f / 5f);

        Destroy(gameObject);
    }

    private void VirtualAnimPlay()
    {
        StartCoroutine(VirtualAnimCoroutine());
    }
    private IEnumerator VirtualAnimCoroutine()
    {
        Color red = Color.red;
        Color white = Color.white;

        float elapsed = 0f;

        while (true)
        {
            elapsed += Time.deltaTime;

            float value = Mathf.PingPong(elapsed / 0.5f, 1f);
            spriteRenderer.color = Color.Lerp(red, white, value);

            yield return null;
        }
    }

    private void OnDestroy()
    {
        Instantiate(
            GameAssetManager.Instance.GameAssets.bombHitboxPrefab,
            transform.position,
            Quaternion.identity);
    }
}