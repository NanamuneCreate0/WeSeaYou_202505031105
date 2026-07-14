using DG.Tweening.Core.Easing;
using UnityEngine;

public class BombAbility : MonoBehaviour
{
    const float explodeAfterSeconds = 4f;

    float timer;
    private void Start()
    {
        timer = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= explodeAfterSeconds)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Debug.Log("爆発");
        Instantiate(
            GameAssetManager.Instance.GameAssets.bombHitboxPrefab,
            transform.position,
            Quaternion.identity);

    }
}