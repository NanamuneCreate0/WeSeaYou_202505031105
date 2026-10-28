using System;
using UnityEngine;

// 特定のIDのときだけ実行される専用イベント
public interface ISpecialEvent
{
    string TargetID { get; }   // このIDのときに自分が動く
    void Execute(ScenarioLine data, Transform chara, Action onComplete);
    void Skip();               // 待機中に入力されたときの処理
}