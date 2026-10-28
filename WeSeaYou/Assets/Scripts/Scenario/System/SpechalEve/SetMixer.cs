using UnityEngine;

public class SetMixer : MonoBehaviour, ISpecialEvent
{
    public string TargetID => "PIX_SPE_CH0T_MIXER";

    [SerializeField] private Transform _mixer;
    [SerializeField] private Transform _destination; // 飛ばし先の目印

    public void Execute(ScenarioLine data, Transform W, System.Action onComplete)
    {
        Debug.Log("SetMixer executed");
        MoveToDestination();
        onComplete?.Invoke(); // 一瞬で終わる処理なので、すぐ呼んでOK
    }

    private void MoveToDestination()
    {
        if (_mixer == null || _destination == null)
        {
            Debug.LogWarning("SetMixer: ミキサーか飛ばし先が未設定です");
            return;
        }

        // X・Yは目印に合わせ、Zは元のまま（2Dの描画順が変わらないように）
        var pos = _destination.position;
        pos.z = _mixer.position.z;
        _mixer.position = pos;
    }

    public void Skip()
    {
        // 一瞬で終わるので、スキップ時にやることはない
    }
}