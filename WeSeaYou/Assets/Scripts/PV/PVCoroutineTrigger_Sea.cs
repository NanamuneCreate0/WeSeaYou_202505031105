using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PVCoroutineTrigger_Sea : MonoBehaviour
{
    [SerializeField] private ShockWaveManager _shockWaveManager;
    [SerializeField] private Transform _sea;
    [SerializeField] private Animator _anim;
    [SerializeField] private CanvasGroup _fade;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(PVCoroutine());
        }
    }

    private IEnumerator PVCoroutine()
    {
        // コルーチン開始時の位置を値として保存
        Vector3 originPosition = _sea.position;

        _anim.Play("PIX_CARA_SEA_WAITP_01_R");
        _shockWaveManager.StartShockWave();

        yield return new WaitForSeconds(2f);

        Vector3 rightPosition =
            originPosition + Vector3.right * 2.5f;

        Vector3 leftPosition =
            originPosition + Vector3.left * 2.5f;

        float moveSpeed = 2.5f;

        _anim.Play("PIX_CARA_SEA_WALKP_01_NOMAL_R");

        yield return _sea
            .DOMove(rightPosition, moveSpeed)
            .SetSpeedBased()
            .SetEase(Ease.OutQuad)
            .WaitForCompletion();

        _anim.Play("PIX_CARA_SEA_WALKP_01_NOMAL_L");

        yield return _sea
            .DOMove(leftPosition, moveSpeed)
            .SetSpeedBased()
            .SetEase(Ease.OutQuad)
            .WaitForCompletion();

        _anim.Play("PIX_CARA_SEA_WAITP_01_R");

        yield return new WaitForSeconds(1f);

        // フェードイン完了まで待つ
        yield return _fade
            .DOFade(1f, 1f)
            .WaitForCompletion();

        _anim.Play("PIX_CARA_SEA_WAIT_01_R");
        _sea.position = originPosition;

        // フェードアウト完了まで待つ
        yield return _fade
            .DOFade(0f, 1f)
            .WaitForCompletion();
    }
}
