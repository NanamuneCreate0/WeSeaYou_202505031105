using System;
using UnityEngine;

public class EndExecutor : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "END";
    public bool IsAutoAdvance => true;
    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        context.ExitScenario();
    }
}
