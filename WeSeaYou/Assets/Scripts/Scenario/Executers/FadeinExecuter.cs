using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;

public class FadeinExecuter : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    public string CommandType => "FADEIN";
    public bool IsAutoAdvance => false;

    [SerializeField] private CanvasGroup _fadeinCanvasGroup; // フェードイン用のCanvasGroup             
    private Tween tween; // Tweenの参照を保持するための変数
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(FadeOutCoroutine(1, null));
        }
    }

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        float duration = float.Parse(data.Param);
        StartCoroutine(FadeOutCoroutine(duration, onComplete));
    }

    private IEnumerator FadeOutCoroutine(float duration, Action onComplete)
    {
        tween = _fadeinCanvasGroup.DOFade(0f, duration); // 指定された期間かけてフェードイン
        yield return tween.WaitForCompletion();
        onComplete?.Invoke();
        if (onComplete == null)
        {
            Debug.Log("Fadein completed without callback.");
        }
    }

    public void HandleAdvanceInput()
    {
        Debug.Log("Advance input received for FadeinExecuter");
        tween.Complete();
    }


}

