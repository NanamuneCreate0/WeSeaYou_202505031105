using System;
using UnityEngine;

public class PicExecutor : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "PIC";
    public bool IsAutoAdvance => true;
    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        Debug.Log($"PICコマンドを実行: {data.Number}:{data.ID}");
        string id = data.ID;
        Debug.Log($"PICコマンドを実行: {data.Number}:{id}");
        if(id.Contains("FACE"))
        {
            Debug.Log($"顔画像を表示: {id}");
            // ここで顔画像を表示する処理を呼び出す
        }
        else if(id.Contains("BODY"))
        {
            Debug.Log($"ボディ画像を表示: {id}");
            // ここでボディ画像を表示する処理を呼び出す
        }
        else
        {
            Debug.LogWarning($"不明な画像ID: {id}");
        }
        onComplete?.Invoke();
    }
}