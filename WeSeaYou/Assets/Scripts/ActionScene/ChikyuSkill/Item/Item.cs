using UnityEngine;

[CreateAssetMenu(menuName = "Item(ScObj)")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int ID;
    //public int[] IDArray = new int[ChikyuSkillCursor.ItemVariety];//�܂������g��Ȃ�//ID��get����IDArray��set�ł����炻��
}
