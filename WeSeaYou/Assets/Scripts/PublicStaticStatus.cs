using UnityEngine;
using System.Collections.Generic;

public static class PublicStaticStatus
{
    public static List<ChikyuSkillItemData> ChikyuSkillItemList = new List<ChikyuSkillItemData>();
    //public static List<StageItemData> StageItemList = new List<StageItemData>();

    public enum Chapter
    {
        None,
        ChapterA,//1-1とか
        ChapterB,//1-2とか
        ChapterC,//2-1とか
        ChapterD,//2-2とか
        ChapterE,//2-3とか
        ChapterF
    }
    public static Chapter ClearedChapter = Chapter.None;
    /*へたちゃんはこのenumを参照して、指定のシナリオをスタートさせる
     * ななむねは、ステージゴール時にenumを+1する
     * はじめてのゲームスタート時、始めるボタンでNone→ChapterA
     */
}
