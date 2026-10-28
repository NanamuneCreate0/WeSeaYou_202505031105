using UnityEngine;
using DG.Tweening;
using System;

public class MoveEvent : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 2.0f; // 1秒あたりに進む距離
    [SerializeField] private float _dushSpeed = 5.0f;

    private Tweener _tween; // 最後に開始した移動（スキップ対象）

    public void WalkTo(float targetPosX, Animator anim, Transform targetChara, Action onArrived)
        => MoveTo(targetPosX, _walkSpeed, anim, targetChara, onArrived);

    public void DushTo(float targetPosX, Animator anim, Transform targetChara, Action onArrived)
        => MoveTo(targetPosX, _dushSpeed, anim, targetChara, onArrived);

    private void MoveTo(float targetPosX, float speed, Animator anim, Transform targetChara, Action onArrived)
    {
        if (targetChara == null)
        {
            Debug.LogWarning($"MoveEvent: 対象がnull (x={targetPosX})");
            onArrived?.Invoke();
            return;
        }

        // 同じキャラがまだ前の移動中なら止める
        targetChara.DOKill();

        _tween = targetChara.DOMoveX(targetPosX, speed)
                            .SetSpeedBased(true)   // 第2引数を「速度」として扱う
                            .SetEase(Ease.Linear)  // 加速・減速なしの等速
                            .OnComplete(() => OnComplete(onArrived, anim));
    }

    private void OnComplete(Action onArrived, Animator anim)
    {
        if (anim != null)
            anim.Play("PIX_CARA_" + anim.name + "_WAIT_01_R");
        onArrived?.Invoke();
    }

    public void ExecuteImmediate()
    {
        _tween?.Complete(); // 最終地点に移動し、OnCompleteも発火する
    }
}