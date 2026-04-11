using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item(ScObj)")]
public class ItemData : ScriptableObject
{
    public int ID;
    public string itemName;
    public Sprite sprite;
    public GameObject prefab;
    public BlockAbility blockAbility;
    public bool IsBlock=false;
}
