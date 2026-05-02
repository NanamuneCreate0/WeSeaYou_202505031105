using System;

// すべてのコマンドが従うべき「契約」
public interface IScenarioCommand
{
    // このコマンドが何か（"TALK", "BRANCH" など）
    string CommandType { get; }
    bool IsAutoAdvance { get; }

    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    void Execute(ScenarioLine data, IScenarioContext context, Action onComplete);
}
