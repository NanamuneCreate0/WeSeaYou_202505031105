using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item(ScObj)")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public GameObject prefab;
    public bool IsBlock=false;
    public int ID;
}
