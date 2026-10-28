using UnityEngine;

public class SetHighLemonTree : MonoBehaviour, ISpecialEvent
{
    public string TargetID => "PIX_SPE_CH0T_HIGHLEMON";

    [SerializeField] private Transform _lemonTree;
    [SerializeField] private Transform _destination; // 飛ばし先の目印

    public void Execute(ScenarioLine data, Transform W, System.Action onComplete)
    {
        Debug.Log("SetHighLemonTree executed");
        MoveToDestination();
        onComplete?.Invoke(); // 一瞬で終わる処理なので、すぐ呼んでOK
    }

    private void MoveToDestination()
    {
        if (_lemonTree == null || _destination == null)
        {
            Debug.LogWarning("SetHighLemonTree: レモンの木か飛ばし先が未設定です");
            return;
        }

        // X・Yは目印に合わせ、Zは元のまま（2Dの描画順が変わらないように）
        var pos = _destination.position;
        pos.z = _lemonTree.position.z;
        _lemonTree.position = pos;
    }

    public void Skip()
    {
        // 一瞬で終わるので、スキップ時にやることはない
    }
}