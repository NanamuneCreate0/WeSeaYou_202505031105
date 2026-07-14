using UnityEngine;

[CreateAssetMenu(menuName = "CollectionItem(ScObj)")]
public class CollectionItemData : ScriptableObject
{
    public int ID;
    public string itemName;
    public Sprite sprite;
}
