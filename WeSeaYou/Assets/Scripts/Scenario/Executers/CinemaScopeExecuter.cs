using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class CinemaScopeExecuter : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    public string CommandType => "CINEMASCOPE";
    public bool IsAutoAdvance => false;

    private const string ID_IN = "CS_IN";
    private const string ID_OUT = "CS_OUT";

    [SerializeField] private CinemaScopeManager _cinemaScopeManager;
    [SerializeField] private RectTransform _portrayalSea;
    [SerializeField] private RectTransform _portrayalMare;

    [Header("Portrayal Tween")]
    [SerializeField] private float _offScreenOffset = 600f;
    [SerializeField] private float _portrayalDuration = 1f;
    [SerializeField] private Ease _portrayalEase = Ease.OutCubic;

    [Header("Timing")]
    [SerializeField] private float _delayBeforePortrayal = 0.75f;
    [SerializeField] private float _delayAfterComplete = 0.5f;

    // エディタで置いた位置 = 立ち位置
    private Vector2 _seaOnPos;
    private Vector2 _mareOnPos;

    private void Awake()
    {
        _seaOnPos = _portrayalSea.anchoredPosition;
        _mareOnPos = _portrayalMare.anchoredPosition;

        // 開始時は画面外へ
        _portrayalSea.anchoredPosition = SeaOffPos;
        _portrayalMare.anchoredPosition = MareOffPos;
    }

    private Vector2 SeaOffPos => _seaOnPos + Vector2.left * _offScreenOffset;
    private Vector2 MareOffPos => _mareOnPos + Vector2.right * _offScreenOffset;

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        StartCoroutine(PlayCoroutine(data.ID, onComplete));
    }

    private IEnumerator PlayCoroutine(string id, Action onComplete)
    {
        switch (id)
        {
            case ID_IN:
                yield return _cinemaScopeManager.PlayOnCinemaScopeCoroutine();
                yield return new WaitForSeconds(_delayBeforePortrayal);
                yield return MovePortrayals(_seaOnPos.x, _mareOnPos.x);
                break;

            case ID_OUT:
                yield return _cinemaScopeManager.PlayOffCinemaScopeCoroutine();
                yield return MovePortrayals(SeaOffPos.x, MareOffPos.x);
                break;

            default:
                Debug.LogWarning($"[CinemaScopeExecuter] 未知のID: {id}");
                break;
        }

        yield return new WaitForSeconds(_delayAfterComplete);
        onComplete?.Invoke();
    }

    private IEnumerator MovePortrayals(float seaX, float mareX)
    {
        _portrayalSea.DOKill();
        _portrayalMare.DOKill();

        Sequence seq = DOTween.Sequence()
            .Join(_portrayalSea.DOAnchorPosX(seaX, _portrayalDuration).SetEase(_portrayalEase))
            .Join(_portrayalMare.DOAnchorPosX(mareX, _portrayalDuration).SetEase(_portrayalEase))
            .SetLink(gameObject);

        yield return seq.WaitForCompletion();
    }

    public void HandleAdvanceInput()
    {
    }
}