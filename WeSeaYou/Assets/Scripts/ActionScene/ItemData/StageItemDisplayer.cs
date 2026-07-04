using UnityEngine;
using UnityEngine.UI;

public class StageItemDisplayer : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Image itemPrefab;

    private void OnEnable()
    {
        StageItemInventory.OnChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        StageItemInventory.OnChanged -= Refresh;
    }

    private void Refresh()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        foreach (StageItemData item in StageItemInventory.Items)
        {
            Image image = Instantiate(itemPrefab, parent);
            image.sprite = item.sprite;
        }
    }
}