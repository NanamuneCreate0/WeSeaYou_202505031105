using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicExecutor : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "PIC";
    public bool IsAutoAdvance => true;
    public bool IsDebugLog = false;

    private const string EMPTY_STATE = "Empty"; // EMO の Animator に作る空ステート

    [SerializeField] private List<Image> _PICParts;
    [SerializeField] private TextExecutor _textExecutor; // TEXT の合図を聞くため

    private readonly HashSet<Image> _activeEmotions = new();

    private void OnEnable()
    {
        if (_textExecutor != null) _textExecutor.OnTextAdvanced += HideAllEmotions;
    }

    private void OnDisable()
    {
        if (_textExecutor != null) _textExecutor.OnTextAdvanced -= HideAllEmotions;
    }

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        string id = data.ID;
        if (IsDebugLog) Debug.Log($"PICコマンドを実行: {data.Number}:{id}");

        foreach (var part in _PICParts)
        {
            if (!id.Contains(part.name)) continue;

            if (part.name.Contains("EMO"))
            {
                ShowEmotion(part, id);
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

    private void ShowEmotion(Image emotionImage, string emotionID)
    {
        emotionImage.enabled = true;
        _activeEmotions.Add(emotionImage);

        Animator animator = emotionImage.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play(emotionID, 0, 0f);
            if (IsDebugLog) Debug.Log($"表情を変更: {emotionID}");
        }
        else
        {
            Debug.LogWarning($"Animatorが見つかりません: {emotionImage.name}");
        }
    }

    private void HideAllEmotions()
    {
        foreach (var emo in _activeEmotions)
        {
            emo.enabled = false;

            Animator animator = emo.GetComponent<Animator>();
            if (animator != null) animator.Play(EMPTY_STATE, 0, 0f);
        }
        _activeEmotions.Clear();
    }
}