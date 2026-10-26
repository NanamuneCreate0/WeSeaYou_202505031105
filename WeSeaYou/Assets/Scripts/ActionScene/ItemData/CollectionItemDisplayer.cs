using UnityEngine;
using UnityEngine.UI;

public class CollectionItemDisplayer : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Image itemPrefab;

    private void OnEnable()
    {
        PublicStaticItem.OnCollectionItemChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        PublicStaticItem.OnCollectionItemChanged -= Refresh;
    }

    private void Refresh()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        foreach (CollectionItemData item in PublicStaticItem.CollectionItemList)
        {
            Image image = Instantiate(itemPrefab, parent);
            image.sprite = item.sprite;
        }
    }
}
