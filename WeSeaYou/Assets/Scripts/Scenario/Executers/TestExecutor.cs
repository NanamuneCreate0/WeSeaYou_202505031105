using System;
using UnityEngine;

public class TestExecutor : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "TEST";
    public bool IsAutoAdvance => true;
    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        Debug.Log(data);
        onComplete?.Invoke();
    }
}
