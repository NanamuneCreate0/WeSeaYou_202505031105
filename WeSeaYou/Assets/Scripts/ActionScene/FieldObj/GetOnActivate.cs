using UnityEngine;

public class GetOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private StageItemData itemData;

    public void Activate()
    {
        if (itemData == null)
        {
            Debug.LogWarning($"{name}: StageItemData‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB");
            return;
        }
        StageItemInventory.Add(itemData);
        Debug.Log($"Get Item: {itemData.itemName}");

        Destroy(gameObject);
    }
}
