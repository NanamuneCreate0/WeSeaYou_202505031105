using UnityEngine;

public class KeyDoorOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private StageItemData keyItem;

    public void Activate()
    {
        if (!StageItemInventory.Contains(keyItem))
        {
            return;
        }

        StageItemInventory.Remove(keyItem);
        Destroy(gameObject);
    }
}