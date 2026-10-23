using UnityEngine;
using UnityEngine.UI;

public class StageItemDisplayer : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Image itemPrefab;

    private void OnEnable()
    {
        PublicStaticItem.OnStageItemChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        PublicStaticItem.OnStageItemChanged -= Refresh;
    }

    private void Refresh()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        foreach (StageItemData item in PublicStaticItem.StageItemList)
        {
            Image image = Instantiate(itemPrefab, parent);
            image.sprite = item.sprite;
        }
    }
}