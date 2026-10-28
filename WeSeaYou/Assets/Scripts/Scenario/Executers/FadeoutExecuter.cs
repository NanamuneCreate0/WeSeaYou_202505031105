using UnityEngine;
using System;
using System.Globalization;
using DG.Tweening;

public class FadeoutExecuter : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    public string CommandType => "FADEOUT";
    public bool IsAutoAdvance => false;

    [SerializeField] private CanvasGroup _fadeoutCanvasGroup;

    private Tween _tween;
    private Action _pendingComplete; // 待機中だけ値が入る

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        if (!float.TryParse(data.Param, NumberStyles.Float, CultureInfo.InvariantCulture, out float duration))
        {
            Debug.LogWarning($"FADEOUTのParamが読めない: {data.Param}（1秒で実行します）");
            duration = 1f;
        }

        _fadeoutCanvasGroup.DOKill(); // 同じCanvasGroupで動いている他のフェードを止める
        _tween = _fadeoutCanvasGroup.DOFade(1f, duration)
                                    .SetUpdate(true); // Time.timeScale の影響を受けない

        if (ShouldWaitForEnd(data))
        {
            // 待機：フェードが終わったら次へ
            _pendingComplete = onComplete;
            _tween.OnComplete(Finish);
        }
        else
        {
            // 素通り：フェードは裏で続けたまま、すぐ次へ
            _pendingComplete = null;
            onComplete?.Invoke();
        }
    }

    private bool ShouldWaitForEnd(ScenarioLine data)
    {
        return data.EventWaitFlag == 1;
    }

    private void Finish()
    {
        _tween = null;
        var cb = _pendingComplete;
        _pendingComplete = null;
        cb?.Invoke();
    }

    public void HandleAdvanceInput()
    {
        if (_pendingComplete == null) return; // 待機中でなければ無視
        _tween?.Complete(); // 最後まで飛ばす → OnComplete で Finish が呼ばれる
    }
}