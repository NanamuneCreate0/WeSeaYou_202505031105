using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChikyuSkillCursor : MonoBehaviour
{
    //注意/////////////////////
    //一旦ここにPublicStaticStatus避難してます
    public static int ItemVariety = 30;//書き換えれてしまうが読み込み専用
    public static List<ChikyuSkillMixtureIndex> InitialMixtureDictionary = new List<ChikyuSkillMixtureIndex>();//書き換えれてしまうが読み込み専用
    public static List<ChikyuSkillMixtureIndex> MixtureDictionary = new List<ChikyuSkillMixtureIndex>();
    //public static List<ChikyuSkillMixtureIndex> InitialMixtureDictionary_Chapter1 = new List<ChikyuSkillMixtureIndex>();//書き換えれてしまうが読み込み専用
    //public static List<ChikyuSkillMixtureIndex> MixtureDictionary_Chapter1 = new List<ChikyuSkillMixtureIndex>();


    public int CursorMode = 0;//0:Hand動かす//1:決定ボタン
    [SerializeField]
    RectTransform ChikyuSkillCursorImage;
    [SerializeField]
    RectTransform ChikyuSkillConfirmButton;
    RectTransform rect;

    void Start()
    {
        rect=GetComponent<RectTransform>();
        ChangeCursorMode(0);
    }
    public void ChangeCursorMode(int num)
    {
        if (num == 0)
        {
            rect.localPosition=ChikyuSkillCursorImage.localPosition;
        }
        else if (num == 1)
        {
            rect.localPosition = ChikyuSkillConfirmButton.localPosition;
        }
        CursorMode = num;
    }
}
