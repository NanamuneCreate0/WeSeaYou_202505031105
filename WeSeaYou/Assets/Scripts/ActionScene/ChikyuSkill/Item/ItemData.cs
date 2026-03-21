using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item(ScObj)")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int ID;
    public List<ItemData> MixedItem=new List<ItemData>();
    public bool IsBlock=false;
}
//public int[] IDArray = new int[ChikyuSkillCursor.ItemVariety];//‚Ü‚Ÿ‘½•ªŽg‚í‚È‚¢//ID‚ðget‚µ‚ÄIDArray‚ðset‚Å‚«‚½‚ç‚»‚ê
