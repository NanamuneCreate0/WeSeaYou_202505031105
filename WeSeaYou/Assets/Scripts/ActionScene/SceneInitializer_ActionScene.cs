using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;

public class SceneInitializer_ActionScene : MonoBehaviour
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
    GameObject FadePannel;

    [SerializeField]
    List<ChikyuSkillItemData> debugItems = new List<ChikyuSkillItemData>();
    [SerializeField] private GameObject chapterAObj;
    [SerializeField] private GameObject chapterBObj;
    [SerializeField] private GameObject chapterCObj;
    [SerializeField] private GameObject otherObj;

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
    public void SetStartObj(GameObject startObj)
    {
        StartObj = startObj;
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

        //Chapter‚É‘Š“–‚·‚éStage‚ð—LŒø‰»void Start()
        {
            switch (PublicStaticStatus.ClearedChapter)
            {
                case PublicStaticStatus.Chapter.ChapterA:
                    chapterAObj.SetActive(true);
                    break;

                case PublicStaticStatus.Chapter.ChapterB:
                    chapterBObj.SetActive(true);
                    break;

                case PublicStaticStatus.Chapter.ChapterC:
                    chapterCObj.SetActive(true);
                    break;

                default:
                    otherObj.SetActive(true);
                    break;
            }
        }
    }
}
