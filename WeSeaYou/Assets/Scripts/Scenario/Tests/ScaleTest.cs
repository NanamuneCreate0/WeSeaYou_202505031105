using UnityEngine;
using DG.Tweening; // © ‚±‚ê‚ªÔ”gü‚É‚È‚Á‚Ä‚¢‚È‚¯‚ê‚Î¬Œ÷I

public class ScaleTest : MonoBehaviour
{
    [SerializeField] private float _targetHeight;
    [SerializeField] private float _duration;
    void Start()
    {
        // 2•b‚©‚¯‚Ä‚‚³‚ğ3‚É‚·‚é
        transform.DOScaleY(_targetHeight, _duration).SetEase(Ease.InOutSine);
    }
}