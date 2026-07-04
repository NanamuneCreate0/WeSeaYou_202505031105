using UnityEngine;

public class KeyDoorOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private StageItemData keyItem;

    public void Activate()
    {
        Debug.Log(StageItemInventory.Contains(keyItem));
        if (!StageItemInventory.Contains(keyItem))
        {
            return;
        }

        StageItemInventory.Remove(keyItem);
        Destroy(gameObject);
    }
}