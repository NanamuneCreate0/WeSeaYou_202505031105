using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer_ActionScene: MonoBehaviour
{
    [SerializeField]
    int CurrentStage;

    [SerializeField]
    List<ChikyuSkillItemData> debugItems = new List<ChikyuSkillItemData>();
    void Start()
    {
        Application.targetFrameRate = 60;
        DebugFunction();
    }
    void DebugFunction()
    {
        //Item‘«‚·
        if (PublicStaticStatus.ChikyuSkillItemList.Count == 0)
        {
            Debug.Log("Get Item for Debug");
            foreach (ChikyuSkillItemData item in debugItems)
            {
                PublicStaticStatus.ChikyuSkillItemList.Add(item);
            }
        }
    }
}
