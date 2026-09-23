using UnityEngine;
using System.Collections.Generic;
using System;

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
    private static int volume_Config = 5;
    public static int Volume_Config
    {
        get
        {
            return volume_Config;
        }
        set
        {

            if (value < 0 || value > 11)
            {
                Debug.LogWarning(
                    $"Volume_Configの値が範囲外です。0～11の範囲で指定してください。指定値: {value}"
                );
                return;
            }
            if (volume_Config == value)
            {
                return;
            }

            volume_Config = value;
            VolumeConfigChanged?.Invoke();
        }
    }
    public static event Action VolumeConfigChanged;
}
