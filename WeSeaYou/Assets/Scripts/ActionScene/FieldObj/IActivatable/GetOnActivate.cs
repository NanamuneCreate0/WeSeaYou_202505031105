using UnityEngine;

public class GetOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private bool isCollectionItem;
    [SerializeField] private StageItemData stageItemToGet;
    [SerializeField] private CollectionItemData collectionItemToGet;

    public void Activate()
    {
        if (isCollectionItem)
        {
            if (collectionItemToGet == null)
            {
                Debug.LogWarning($"{name}: CollectionItemDataÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");
                return;
            }

            SEManager_Nana.Instance.PlaySE(GameAssetManager.Instance.GameAssets.getSE);
            PublicStaticItem.AddCollectionItem(collectionItemToGet);
            Debug.Log($"Get Collection Item: {collectionItemToGet.itemName}");
        }
        else
        {
            if (stageItemToGet == null)
            {
                Debug.LogWarning($"{name}: StageItemDataÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");
                return;
            }

            SEManager_Nana.Instance.PlaySE(GameAssetManager.Instance.GameAssets.getSE);
            PublicStaticItem.AddStageItem(stageItemToGet);
            Debug.Log($"Get Item: {stageItemToGet.itemName}");
        }

        Destroy(gameObject);

    }
}
