using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;

public class MoveEvent : MonoBehaviour
{
    private Tweener _tween;

    public void WalkTo(float targetPosX, Animator anim, Transform targetChara, Action onArrived)
    {
        float speed = 2.0f; // 移動速度を設定
        if (targetChara == null)
        {
            Debug.Log(targetPosX);
            onArrived();
            return;
        }

        _tween = targetChara.DOMoveX(targetPosX, speed).OnComplete(() => OnComplete(onArrived, anim));
    }

    public void DushTo(float targetPosX, Animator anim, Transform targetChara, Action onArrived)
    {
        float speed = 1.0f; // 移動速度を設定
        if (targetChara == null)
        {
            Debug.Log(targetPosX);
            onArrived();
            return;
        }

        _tween = targetChara.DOMoveX(targetPosX, speed).OnComplete(() => OnComplete(onArrived, anim));
    }

    private void OnComplete(Action onArrived, Animator anim)
    {
        anim.Play("PIX_CARA_" + anim.name + "_WAIT_01_R"); // Waitアニメーションを再生
        onArrived();
    }

    public void ExecuteImmediate()
    {
        _tween?.Complete();   // 位置が最終地点になり、OnCompleteも発火する
    }
}
