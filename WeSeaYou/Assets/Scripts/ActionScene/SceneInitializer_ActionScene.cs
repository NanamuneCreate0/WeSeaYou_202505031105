using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer_ActionScene: MonoBehaviour
{
    [SerializeField]
    int CurrentStage;

    [SerializeField]
    List<Item> debugItems = new List<Item>();
    [SerializeField]
    List<ChikyuSkillMixtureIndex> indexes = new List<ChikyuSkillMixtureIndex>();
    void Start()
    {
        PublicStaticStatus.CurrentStage = CurrentStage;
        Application.targetFrameRate = 60;
        DebugFunction();
    }
    void DebugFunction()
    {
        //Item‘«‚·
        foreach (Item item in debugItems)
        {
            PublicStaticStatus.ItemList.Add(item);
        }
        GameObject.Find("ItemDisplayer").GetComponent<ItemDisplayer>().SetItemDisplay(true);

        //Index‘«‚·
        for (int i = 0; i < indexes.Count; i++)
        {
            ChikyuSkillCursor.MixtureDictionary.Add(indexes[i]);
            //Debug.Log(indexes[i] + " - On Index");
        }
    }
}
