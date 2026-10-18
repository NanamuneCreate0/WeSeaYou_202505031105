using System.Collections;
using UnityEngine;

public class Grass_FieldObj : MonoBehaviour
{
    private Animator animator;

    private IEnumerator Start()
    {
        animator = GetComponent<Animator>();

        // Animatorが最初のステートを再生するまで待つ
        yield return null;

        // 現在再生しているステートを取得
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // ランダムな位置から再生する
        animator.Play(stateInfo.fullPathHash, 0, Random.value);
    }

    [ContextMenu("Snap Position")]
    private void SnapPositionToGrid()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Round(pos.x / 0.03125f) * 0.03125f;
        pos.y = Mathf.Round(pos.y / 0.03125f) * 0.03125f;

        transform.position = pos;
    }
}
