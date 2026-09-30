using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;

public class SceneInitializer_ActionScene: MonoBehaviour
{
    [System.Serializable]
    public class StartObjectToPlace
    {
        public GameObject obj;
        public Vector3 offset;
    }
    [SerializeField]
    List<StartObjectToPlace> objects;
    [SerializeField]
    GameObject StartObj;

    [SerializeField]
    List<ChikyuSkillItemData> debugItems = new List<ChikyuSkillItemData>();
    void Start()
    {
        Application.targetFrameRate = 60;
        DebugFunction();
        foreach (StartObjectToPlace so in objects)
        {
            if (so.obj != null)
            {
                so.obj.transform.position = StartObj.transform.position + so.offset;
            }
        }
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
