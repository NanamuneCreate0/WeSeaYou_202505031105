using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CinemaScopeManager : MonoBehaviour
{
    [SerializeField] private RectTransform _topBar;
    [SerializeField] private RectTransform _bottomBar;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _targetHeight = 1f;

    private CanvasGroup _groupTop;
    private CanvasGroup _groupBottom;

    private void Start()
    {
        _groupTop = _topBar.GetComponent<CanvasGroup>();
        _groupBottom = _bottomBar.GetComponent<CanvasGroup>();
        /*if (_groupTop == null)
        {
            _groupTop = _topBar.gameObject.AddComponent<CanvasGroup>();
        }
        if (_groupBottom == null)
        {
            _groupBottom = _bottomBar.gameObject.AddComponent<CanvasGroup>();
        }*/
    }

    public IEnumerator PlayOnCinemaScopeCoroutine()
    {
        _topBar.DOScaleY(-_targetHeight, _duration).SetEase(Ease.InOutSine);
        _bottomBar.DOScaleY(_targetHeight, _duration).SetEase(Ease.InOutSine);
        if (_groupTop != null || _groupBottom != null)
        {
            _groupTop.DOFade(1f, _duration).SetEase(Ease.InOutSine);
            _groupBottom.DOFade(1f, _duration).SetEase(Ease.InOutSine);
        }
        yield return null;
    }
    public IEnumerator PlayOffCinemaScopeCoroutine()
    {
        _topBar.DOScaleY(0f, _duration).SetEase(Ease.InOutSine);
        _bottomBar.DOScaleY(0f, _duration).SetEase(Ease.InOutSine);
        if (_groupTop != null || _groupBottom != null)
        {
            _groupTop.DOFade(0f, _duration).SetEase(Ease.InOutSine);
            _groupBottom.DOFade(0f, _duration).SetEase(Ease.InOutSine);
        }
        yield return null;
    }
}
