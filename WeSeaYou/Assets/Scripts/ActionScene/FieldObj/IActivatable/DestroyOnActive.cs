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
        SEManager_Nana.Instance.PlaySE(GameAssetManager.Instance.GameAssets.bombSE);
        Instantiate(
            GameAssetManager.Instance.GameAssets.bombHitboxPrefab2,
            transform.position,
            Quaternion.identity);
    }
}