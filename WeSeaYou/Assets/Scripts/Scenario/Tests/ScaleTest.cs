using UnityEngine;
using DG.Tweening;
using System.Collections; // © ‚±‚ê‚ªÔ”gü‚É‚È‚Á‚Ä‚¢‚È‚¯‚ê‚Î¬Œ÷I

public class ScaleTest : MonoBehaviour
{
    [SerializeField] private float _targetHeight;
    [SerializeField] private float _duration;

    private CanvasGroup _group;
    void Start()
    {
        // 2•b‚©‚¯‚Ä‚‚³‚ğ3‚É‚·‚é
        _group = GetComponent<CanvasGroup>();
        transform.DOScaleY(_targetHeight, _duration).SetEase(Ease.InOutSine);
        if(_group != null) _group.DOFade(1f, _duration).SetEase(Ease.InOutSine);
    }

    
}