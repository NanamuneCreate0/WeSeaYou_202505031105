using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using System.Collections;


public class FixDTIEEvent : MonoBehaviour, ISpecialEvent
{
    public string TargetID => "PIX_CARA_W_TIE";
    [SerializeField] private Transform _sea;
    [SerializeField] private Transform _mare;

    public void Execute(ScenarioLine data, Transform W, Action onComplete)
    {
        // 専用の動き
        Debug.Log("FixDTIEEvent executed");
        StartCoroutine(FixDTIECorutine(W, onComplete));
        //onComplete?.Invoke();
    }

    private IEnumerator FixDTIECorutine(Transform W, Action onComplete)
    {
        Animator animator_sea = _sea.GetComponent<Animator>();
        Animator animator_mare = _mare.GetComponent<Animator>();
        Animator animator_W = W.GetComponent<Animator>();

        animator_mare.Play("PIX_CARA_MARE_WALK_01_NOMAL_L");
        yield return _mare.DOMoveX(-0.74f, 2).WaitForCompletion();

        _sea.gameObject.SetActive(false);
        _mare.gameObject.SetActive(false);
        W.gameObject.SetActive(true);
        animator_W.Play("PIX_CARA_W_TIE");
        yield return new WaitForSeconds(2.0f);

        _sea.gameObject.SetActive(true);
        _mare.gameObject.SetActive(true);
        W.gameObject.SetActive(false);
        animator_mare.Play("PIX_CARA_MARE_WALK_01_NOMAL_R");
        animator_sea.Play("PIX_CARA_SEA_STAND_HAPPY_01_W");
        yield return _mare.DOMoveX(1.48f, 2).WaitForCompletion();

        animator_mare.Play("PIX_CARA_MARE_STAND_STUNNED_01_TIE");
        onComplete?.Invoke();

    }

    public void Skip() { /* 最終状態に飛ばす */ }
}
