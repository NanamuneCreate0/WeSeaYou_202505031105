using System.Collections;
using UnityEngine;

public class BombAnimEvent : MonoBehaviour, ISpecialEvent
{
    public string TargetID => "PIX_SPE_CH0T_EXPLOSION";

    private const string STATE_NAME = "BombAnimationAnimation";
    private const float TIMEOUT = 5f; // 保険：これ以上かかったら強制終了

    [SerializeField] private Animator _bombAnim;

    private Coroutine _playCoroutine;

    public void Execute(ScenarioLine data, Transform W, System.Action onComplete)
    {
        if (_playCoroutine != null) StopCoroutine(_playCoroutine);

        _bombAnim.gameObject.SetActive(true);
        _playCoroutine = StartCoroutine(WaitForAnimationEnd());

        // 爆発は裏で再生、シナリオはすぐ次へ
        onComplete?.Invoke();
    }

    private IEnumerator WaitForAnimationEnd()
    {
        _bombAnim.Play(STATE_NAME, 0, 0f);
        yield return null;

        float elapsed = 0f;
        while (true)
        {
            AnimatorStateInfo info = _bombAnim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName(STATE_NAME) && info.normalizedTime >= 1f) break;

            elapsed += Time.deltaTime;
            if (elapsed >= TIMEOUT)
            {
                Debug.LogWarning($"爆発アニメが終わらないので強制終了: {STATE_NAME}");
                break;
            }
            yield return null;
        }

        _bombAnim.gameObject.SetActive(false);
        _playCoroutine = null;
    }

    public void Skip()
    {
        // 裏で再生して勝手に消えるので、スキップ時にやることはない
    }
}