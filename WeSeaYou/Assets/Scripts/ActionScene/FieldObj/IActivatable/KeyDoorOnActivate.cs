using UnityEngine;

public class KeyDoorOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private StageItemData keyItem;

    public void Activate()
    {
        Debug.Log(PublicStaticItem.ContainsStageItem(keyItem));
        if (!PublicStaticItem.ContainsStageItem(keyItem))
        {
            return;
        }

        PublicStaticItem.RemoveStageItem(keyItem);
        Destroy(gameObject);
    }
}