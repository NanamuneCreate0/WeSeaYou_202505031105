using DG.Tweening;
using System.Collections;
using System;
using UnityEngine;

public class BalkWalkEvent : MonoBehaviour, ISpecialEvent
{
    public string TargetID => "PIX_CARA_MARE_BACKWALK_NOMAL_01";
    [SerializeField] private Transform _mare;
    [SerializeField] private Transform _destination; // 飛ばし先の目印
    private Animator _animator_mare;
    public void Execute(ScenarioLine data, Transform W, Action onComplete)
    {
        Debug.Log("BalkWalkEvent executed");
        _animator_mare = _mare.GetComponent<Animator>();
        StartCoroutine(MoveToDestination(onComplete));
        //onComplete?.Invoke(); // 一瞬で終わる処理なので、すぐ呼んでOK
    }
    private IEnumerator MoveToDestination(Action onComplete)
    {
        if (_mare == null)
        {
            Debug.LogWarning("BalkWalkEvent: マ-レか飛ばし先が未設定です");
            yield return null;
        }

        _animator_mare.Play("PIX_CARA_MARE_WALK_01_NOMAL_R");
        yield return _mare.DOMoveX(_destination.transform.position.x, 4f).WaitForCompletion();

        _animator_mare.Play("PIX_CARA_MARE_WAIT_01_R");

        onComplete?.Invoke();
    }
    public void Skip()
    {
        // 一瞬で終わるので、スキップ時にやることはない
    }    
}
