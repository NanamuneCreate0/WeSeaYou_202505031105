using UnityEngine;

public class DestroyOnActivate : MonoBehaviour, IActivatable
{
    public void Activate()
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