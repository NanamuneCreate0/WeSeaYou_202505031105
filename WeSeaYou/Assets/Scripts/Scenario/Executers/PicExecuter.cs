using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI; // ← Image を使うのに必要

public class PicExecutor : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "PIC";
    public bool IsAutoAdvance => true;
    public bool IsDebugLog = false;

    [SerializeField] private List<Image> _PICParts; // Transform → Image

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        string id = data.ID;
        if (IsDebugLog) Debug.Log($"PICコマンドを実行: {data.Number}:{id}");

        foreach (var part in _PICParts)
        {
            if (!id.Contains(part.name)) continue;

            if (part.name.Contains("EMO"))
            {
                StartCoroutine(ActionEmotionCoroutine(part, id));
            }
            else
            {
                ChangePicturePart(id, part);
            }
            break;
        }
        onComplete?.Invoke();
    }

    private void ChangePicturePart(string id, Image image)
    {
        Sprite newSprite = Resources.Load<Sprite>($"Sprites/CharaParts/{id}");
        if (newSprite == null)
        {
            Debug.LogWarning($"画像が見つかりません: {id}");
            return;
        }

        image.sprite = newSprite;
        if (IsDebugLog) Debug.Log($"画像を変更: {id}");
    }

    private IEnumerator ActionEmotionCoroutine(Image emotionImage, string emotionID)
    {
        Animator animator = emotionImage.GetComponent<Animator>();

        // 前回のフェードを止めて、透明度を戻してから表示
        emotionImage.DOKill();
        Color c = emotionImage.color;
        c.a = 1f;
        emotionImage.color = c;
        emotionImage.gameObject.SetActive(true);

        if (animator != null)
        {
            animator.Play(emotionID, 0, 0f); // Animator内のステート名で再生
            if (IsDebugLog) Debug.Log($"表情を変更: {emotionID}");
        }
        else
        {
            Debug.LogWarning($"Animatorが見つかりません: {emotionImage.name}");
        }

        yield return new WaitForSeconds(2f);

        yield return emotionImage.DOFade(0f, 1f)
            .SetLink(emotionImage.gameObject)
            .WaitForCompletion();

        emotionImage.gameObject.SetActive(false);
    }
}