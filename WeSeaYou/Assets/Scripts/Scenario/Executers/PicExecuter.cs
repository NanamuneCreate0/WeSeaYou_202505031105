using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicExecutor : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "PIC";
    public bool IsAutoAdvance => true;
    // コマンドを実行する
    // onComplete: 完了時に呼ぶコールバック
    [SerializeField] private List<Transform> _PICParts; // 例: 顔画像やボディ画像などのTransformリスト
    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        string id = data.ID;
        Debug.Log($"PICコマンドを実行: {data.Number}:{id}");
    
        foreach (var part in _PICParts)
        {
            if (id.Contains(part.name)) // 例: "CHARACTER1_FACE_HAPPY"が"FACE"を含む場合
            {
                if(part.name.Contains("EMO"))
                {
                    StartCoroutine(ActionEmotionCoroutine(part, id)); // 表情を変更する処理を呼び出す
                }
                else
                {
                    Debug.Log($"画像を表示: {id}");
                    Transform targetPart = part; // 例: 対応する画像のTransformを取得する
                    ChangePicturePart(id, targetPart); // 画像を変更する処理を呼び出す
                }
                break; // 一致する部分が見つかったらループを抜ける
            }
        }
        onComplete?.Invoke();
    }

    private void ChangePicturePart(string id, Transform part)
    {
        // ここでpartの画像をidに基づいて変更する処理を実装する
        // 例: SpriteRendererのspriteを変更するなど
        SpriteRenderer renderer = part.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            Sprite newSprite = Resources.Load<Sprite>($"Sprites/CharaParts/{id}"); // 例: "Pictures/CHARACTER1_FACE_HAPPY"
            if (newSprite != null)
            {
                renderer.sprite = newSprite;
                Debug.Log($"画像を変更: {id}");
            }
            else
            {
                Debug.LogWarning($"画像が見つかりません: {id}");
            }
        }
        else
        {
            Debug.LogWarning($"SpriteRendererが見つかりません: {part.name}");
        }
    }
    
    private IEnumerator ActionEmotionCoroutine(Transform emotionTransform, String emotionID)
    {
        SpriteRenderer emotionSprite = emotionTransform.GetComponent<SpriteRenderer>();
        Animator animator = emotionTransform.GetComponent<Animator>();
        /*emotionSprite.gameObject.SetActive(true);
        AnimationController clip = Resources.Load<AnimationController>($"Animations/EMOAnimators/{emotionID}"); // 例: "Animations/Emotions/CHARACTER1_FACE_HAPPY"
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
        emotionSprite.DOFade(0f, 1f).OnComplete(() => emotionSprite.gameObject.SetActive(false));*/ // 例: 表情をフェードアウトして非表示にする
        yield return null; // 処理が完了するまで待機する
    }
}