using UnityEngine;

public class KeyDoorOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private StageItemData keyItem;

    public void Activate()
    {
        Debug.Log(PublicStaticItem.ContainsStageItem(keyItem));
        SEManager_Nana.Instance.PlaySE(GameAssetManager.Instance.GameAssets.decideSE);
        if (!PublicStaticItem.ContainsStageItem(keyItem))
        {
            return;
        }

        PublicStaticItem.RemoveStageItem(keyItem);
        Destroy(gameObject);
    }
}