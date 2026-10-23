using UnityEngine;

public class GetOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private bool isCollectionItem;
    [SerializeField] private StageItemData stageItemToGet;
    [SerializeField] private CollectionItemData collectionItemToGet;

    public void Activate()
    {
        /*if (itemData == null)
        {
            Debug.LogWarning($"{name}: StageItemDataÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");
            return;
        }
        PublicStaticItem.AddStageItem(itemData);
        Debug.Log($"Get Item: {itemData.itemName}");

        Destroy(gameObject);*/

        if (isCollectionItem)
        {
            if (collectionItemToGet == null)
            {
                Debug.LogWarning($"{name}: CollectionItemDataÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅB");
                return;
            }

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

            PublicStaticItem.AddStageItem(stageItemToGet);
            Debug.Log($"Get Item: {stageItemToGet.itemName}");
        }

        Destroy(gameObject);

    }
}
