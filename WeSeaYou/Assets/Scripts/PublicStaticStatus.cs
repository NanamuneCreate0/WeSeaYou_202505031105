using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;

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



    private static bool swapABXY = true;
    public static bool SwapABXY
    {
        get { return swapABXY; }
        set
        {
            swapABXY = value;

            InputAction decide0 = InputManager.Instance.actions.Player.Decide;
            InputAction cancel0 = InputManager.Instance.actions.Player.Jump;
            InputAction decide1 = InputManager.Instance.actions.UI_Nana.Decide;
            InputAction cancel1 = InputManager.Instance.actions.UI_Nana.Cancel;

            InputAction ChikyuSkill = InputManager.Instance.actions.Player.ChikyuSkill;
            InputAction SeaSkill = InputManager.Instance.actions.Player.SeaSkill;
            InputAction SkillRoutine = InputManager.Instance.actions.Player.SkillRoutine;

            if (value)
            {
                // AとBを入れ替える
                decide0.ApplyBindingOverride(0, "<Gamepad>/buttonEast");
                cancel0.ApplyBindingOverride(0, "<Gamepad>/buttonSouth");
                decide1.ApplyBindingOverride(0, "<Gamepad>/buttonEast");
                cancel1.ApplyBindingOverride(0, "<Gamepad>/buttonSouth");
                //XYを入れ替える
                ChikyuSkill.ApplyBindingOverride(0, "<Gamepad>/buttonWest");
                SeaSkill.ApplyBindingOverride(0, "<Gamepad>/buttonNorth");
                SkillRoutine.ApplyBindingOverride(0, "<Gamepad>/buttonNorth");
            }
            else
            {
                // デフォルトに戻す
                decide0.RemoveAllBindingOverrides();
                cancel0.RemoveAllBindingOverrides();
                decide1.RemoveAllBindingOverrides();
                cancel1.RemoveAllBindingOverrides();
                ChikyuSkill.RemoveAllBindingOverrides();
                SeaSkill.RemoveAllBindingOverrides();
                SkillRoutine.RemoveAllBindingOverrides();
            }

            ChangeKeyConfig?.Invoke();
        }
    }
}
