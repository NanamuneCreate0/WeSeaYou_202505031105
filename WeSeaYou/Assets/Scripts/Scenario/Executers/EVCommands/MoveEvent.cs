using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;

public class MoveEvent : MonoBehaviour
{
    private Tweener _tween;

    public void WalkTo(float targetPosX, bool onDush, Transform targetChara, Action onArrived)
    {
        float speed = 2.0f; // 移動速度を設定
        if (targetChara == null)
        {
            Debug.Log(targetPosX);
            onArrived();
            return;
        }

        _tween = targetChara.DOMoveX(targetPosX, speed).OnComplete(() => onArrived());
    }

    public void ExecuteImmediate()
    {
        _tween?.Complete();   // 位置が最終地点になり、OnCompleteも発火する
    }
}
