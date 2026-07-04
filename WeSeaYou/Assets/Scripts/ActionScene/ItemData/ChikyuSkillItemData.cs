using UnityEngine;

[CreateAssetMenu(menuName = "ChikyuSkillItem(ScObj)")]
public class ChikyuSkillItemData : ScriptableObject
{
    public int ID;
    public string itemName;
    public Sprite sprite;
    public GameObject prefab;
    public BlockAbility blockAbility;
    public bool IsBlock=false;
}
