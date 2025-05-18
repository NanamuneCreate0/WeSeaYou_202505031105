using UnityEngine;

[CreateAssetMenu(menuName = "Item(ScObj)")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int ID;
    //public int[] IDArray = new int[ChikyuSkillCursor.ItemVariety];//‚Ü‚Ÿ‘½•ªŽg‚í‚È‚¢//ID‚ðget‚µ‚ÄIDArray‚ðset‚Å‚«‚½‚ç‚»‚ê
}
