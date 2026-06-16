using System;
using UnityEngine;

public class SEExecutor : MonoBehaviour, IScenarioCommand
{
    [SerializeField] private AudioSource _SESource; // AudioManagerへの参照
    public string CommandType => "SE";
    public bool IsAutoAdvance => true;
    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        Debug.Log($"SE Command Executed with data: {data}");
        // ここでSEの再生処理を実装する
        if (_SESource == null) return;
        AudioClip clip = Resources.Load<AudioClip>(data.ID); // 例: "SE/Click"
        _SESource.clip = clip;
        _SESource.Play();
        onComplete?.Invoke();
    }
}