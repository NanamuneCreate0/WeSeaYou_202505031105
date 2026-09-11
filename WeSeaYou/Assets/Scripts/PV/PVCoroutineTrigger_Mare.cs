using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PVCoroutineTrigger_Mare : MonoBehaviour
{
    //[SerializeField] private ShockWaveManager _shockWaveManager;
    [SerializeField] private Transform _mare;
    [SerializeField] private Animator _anim;
    [SerializeField] private CanvasGroup _fade;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(PVCoroutine());
        }
    }

    private IEnumerator PVCoroutine()
    {
        // コルーチン開始時の位置を値として保存
        Vector3 originPosition = _mare.position;

        _anim.Play("PIX_CARA_MARE_WAITP_01_L");
        //_shockWaveManager.StartShockWave();

        yield return new WaitForSeconds(2f);

        Vector3 rightPosition =
            originPosition + Vector3.right * 2.5f;

        Vector3 leftPosition =
            originPosition + Vector3.left * 2.5f;

        float moveSpeed = 2.5f;

        _anim.Play("PIX_CARA_MARE_WALKP_01_NOMAL_L");

        yield return _mare
            .DOMove(leftPosition, moveSpeed)
            .SetSpeedBased()
            .SetEase(Ease.OutQuad)
            .WaitForCompletion();

        _anim.Play("PIX_CARA_MARE_WALKP_01_NOMAL_R");

        yield return _mare
            .DOMove(rightPosition, moveSpeed)
            .SetSpeedBased()
            .SetEase(Ease.OutQuad)
            .WaitForCompletion();

        _anim.Play("PIX_CARA_MARE_WAITP_01_L");

        yield return new WaitForSeconds(1f);

        // フェードイン完了まで待つ
        yield return _fade
            .DOFade(1f, 1f)
            .WaitForCompletion();

        _anim.Play("PIX_CARA_MARE_WAIT_01_L");
        _mare.position = originPosition;

        // フェードアウト完了まで待つ
        yield return _fade
            .DOFade(0f, 1f)
            .WaitForCompletion();
    }
}
