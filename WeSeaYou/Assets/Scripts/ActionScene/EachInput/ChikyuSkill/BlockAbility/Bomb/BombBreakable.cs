using UnityEngine;

public class BombBreakable : MonoBehaviour, IBombTarget
{
    public void OnBombHit()
    {
        OnExplod();
        Destroy(gameObject);
    }
    private void OnExplod()
    {
        Instantiate(
            GameAssetManager.Instance.GameAssets.bombHitboxPrefab2,
            transform.position,
            Quaternion.identity);
    }
}
