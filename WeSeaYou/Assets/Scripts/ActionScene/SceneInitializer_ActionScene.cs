using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer_ActionScene: MonoBehaviour
{
    [SerializeField]
    int CurrentStage;

    [SerializeField]
    List<Item> debugItems = new List<Item>();
    void Start()
    {
        PublicStaticStatus.CurrentStage = CurrentStage;
        Application.targetFrameRate = 60;
        DebugFunction();
    }
    void DebugFunction()
    {
        foreach (Item item in debugItems)
        {
            PublicStaticStatus.ItemList.Add(item);
        }
        GameObject.Find("ItemDisplayer").GetComponent<ItemDisplayer>().SetItemDisplay();
    }
}
