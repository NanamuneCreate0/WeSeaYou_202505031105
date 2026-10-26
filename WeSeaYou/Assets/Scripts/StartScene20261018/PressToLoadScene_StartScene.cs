using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class PressToLoadScene_StartScene : MonoBehaviour
{
    [SerializeField] private AudioClip BGM;
    [SerializeField] Animator fadeAnimator;
    private System.IDisposable buttonPressListener;
    private bool hasPressed = false;

    [SerializeField] private AudioClip gameStartSE;
    private void OnEnable()
    {
        hasPressed = false;

        buttonPressListener =
            InputSystem.onAnyButtonPress.Call(OnAnyButtonPressed);

        BGMManager_ActionScene.Instance.PlayBGM(BGM);
    }

    private void OnDisable()
    {
        buttonPressListener?.Dispose();
        buttonPressListener = null;
    }

    private void OnAnyButtonPressed(InputControl control)
    {
        if (hasPressed)
        {
            return;
        }
        if (control is not ButtonControl)
        {
            return;
        }
        hasPressed = true;
        DebugFunction();
        StartCoroutine(GameStartCoroutine());

    }
    IEnumerator GameStartCoroutine()
    {
        SEManager_Nana.Instance.PlaySE(gameStartSE);
        float constFloat = 2f;
        fadeAnimator.speed = 1 / constFloat;
        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(constFloat);

        SceneManager.LoadScene("StillScene01");
    }
    private void DebugFunction()
    {
        PublicStaticItem.CollectionItemList.Clear();
        PublicStaticStatus.ClearedChapter = PublicStaticStatus.Chapter.ChapterA;
    }
}
