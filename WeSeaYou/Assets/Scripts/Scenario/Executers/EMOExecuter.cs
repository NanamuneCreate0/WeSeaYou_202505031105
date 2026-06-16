using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EMOExecutor : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "EMO";
    public bool IsAutoAdvance => true;
    [SerializeField] private List<Transform> _emotionPos; // 例: 顔の表情や体のポーズなどのTransformリスト
    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        Debug.Log(data);
        foreach (var emo in _emotionPos)
        {
            if (data.ID.Contains(emo.name)) // 例: "CHARACTER1_FACE_HAPPY"が"FACE"を含む場合
            {
                Debug.Log($"表情を変更: {data.ID}");
                Transform targetEmo = emo; // 例: 対応する表情のTransformを取得する
                StartCoroutine(ChangeEmotionCoroutine(emo, data.ID)); // 表情を変更する処理を呼び出す
                break; // 一致する部分が見つかったらループを抜ける
            }
        }
        onComplete?.Invoke();
    }

    private IEnumerator ChangeEmotionCoroutine(Transform emotionTransform, String emotionID)
    {
        SpriteRenderer emotionSprite = emotionTransform.GetComponent<SpriteRenderer>();
        Animator animator = emotionTransform.GetComponent<Animator>();
        emotionSprite.gameObject.SetActive(true);
        Animation clip = Resources.Load<Animation>($"Animations/EMOAnimators/{emotionID}"); // 例: "Animations/Emotions/CHARACTER1_FACE_HAPPY"
        if (animator != null && clip != null)
        {
            animator.Play(clip.name);
            Debug.Log($"表情を変更: {emotionID}");
        }
        else
        {
            Debug.LogWarning($"アニメーターまたはアニメーションクリップが見つかりません: {emotionID}");
        }
        yield return new WaitForSeconds(2f);
        emotionSprite.DOFade(0f, 1f).OnComplete(() => emotionSprite.gameObject.SetActive(false)); // 例: 表情をフェードアウトして非表示にする
        yield return null; // 処理が完了するまで待機する
    }
}
