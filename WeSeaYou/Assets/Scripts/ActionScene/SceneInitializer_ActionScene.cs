using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private AudioClip BGM;
    [SerializeField] private AudioClip BGM2;

    [SerializeField] private GameModeController_ActionScene gameModeController;
    [SerializeField] private ActionModeChanger actionModeChanger;
    void Start()
    {
        Application.targetFrameRate = 60;
        gameModeController.ChangeGameMode(GameModeController_ActionScene.GameMode.Action, true);
        actionModeChanger.ChangeActionMode(ActionModeChanger.ActionModeType.Neutral, ActionModeChanger.ActionModeType.Neutral);
        DebugFunction();
        //Chapter‚É‘Š“–‚·‚éStage‚ð—LŒø‰»
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
        foreach (StartObjectToPlace so in objects)
        {
            if (so.obj != null)
            {
                so.obj.transform.position = StartObj.transform.position + so.offset;
            }
        }

        //BGM
        BGMManager_ActionScene.Instance.PlayBGM(BGM);
        SubBGMManager_ActionScene.Instance.PlayBGM(BGM2);
    }
    public void SetStartObj(GameObject startObj)
    {
        StartObj = startObj;
    }
    void DebugFunction()
    {
        //Item‘«‚·
        if (PublicStaticItem.ChikyuSkillItemList.Count == 0)
        {
            Debug.Log("Get Item for Debug");
            foreach (ChikyuSkillItemData item in debugItems)
            {
                PublicStaticItem.ChikyuSkillItemList.Add(item);
            }
        }
        else
        {
            Debug.LogWarning("Failed To Get Item");
        }

    }
}
