public interface IScenarioContext
{
    void JumpTo(int index);          // 指定行へジャンプ
    void EnterWait();                 // Wait 状態へ
    void ResumeScenario();            // Wait 解除
    void ExitScenario();              // シナリオ終了
}