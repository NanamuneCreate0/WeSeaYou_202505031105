using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer_ActionScene: MonoBehaviour
{
    [SerializeField]
    int CurrentStage;

    [SerializeField]
    List<ItemData> debugItems = new List<ItemData>();
    void Start()
    {
        PublicStaticStatus.CurrentStage = CurrentStage;
        Application.targetFrameRate = 60;
        DebugFunction();
    }
    void DebugFunction()
    {
        //Item‘«‚·
        foreach (ItemData item in debugItems)
        {
            PublicStaticStatus.ItemList.Add(item);
        }
    }
}
