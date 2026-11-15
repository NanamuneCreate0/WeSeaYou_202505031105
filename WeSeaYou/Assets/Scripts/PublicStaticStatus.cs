using UnityEngine;
using System.Collections.Generic;
using System;

public static class PublicStaticStatus
{
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



    //音量
    public static event Action VolumeConfigChanged;
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



    //キーコンフィグ
    public static bool FlipABXY = false;//未実装

    public static event Action ChangeKeyConfig;

    private static bool skillRoutine_Config = true;
    public static bool SkillRoutine_Config
    {
        get { return skillRoutine_Config; }
        set
        {
            skillRoutine_Config = value;
            ChangeKeyConfig?.Invoke();
        }
    }

    private static bool easyChikyuSkill_Config = true;
    public static bool EasyChikyuSkill_Config
    {
        get { return easyChikyuSkill_Config; }
        set
        {
            easyChikyuSkill_Config = value;
            ChangeKeyConfig?.Invoke();
        }
    }

    private static bool easyUtyuSkill_Config = true;
    public static bool EasyUtyuSkill_Config
    {
        get { return easyUtyuSkill_Config; }
        set
        {
            easyUtyuSkill_Config = value;
            ChangeKeyConfig?.Invoke();
        }
    }
}
