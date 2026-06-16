using System;
using UnityEngine;

public class BGMExecutor : MonoBehaviour, IScenarioCommand
{
    [SerializeField] private AudioSource _BGSource; // AudioManagerへの参照
    public string CommandType => "BGM";
    public bool IsAutoAdvance => true;
    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        Debug.Log($"BGM Command Executed with data: {data}");
        // ここでBGMの再生処理を実装する
        if (_BGSource == null) return;
        AudioClip clip = Resources.Load<AudioClip>(data.ID); // 例: "BGM/Theme"
        _BGSource.clip = clip;
        _BGSource.Play();
        onComplete?.Invoke();
    }
}