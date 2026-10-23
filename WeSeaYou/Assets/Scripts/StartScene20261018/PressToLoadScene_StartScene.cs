using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class PressToLoadScene_StartScene : MonoBehaviour
{
    [SerializeField] Animator fadeAnimator;
    private System.IDisposable buttonPressListener;
    private bool hasPressed = false;

    private void OnEnable()
    {
        hasPressed = false;

        buttonPressListener =
            InputSystem.onAnyButtonPress.Call(OnAnyButtonPressed);
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
        Debug.Log("PressAny‚¨‚³‚ê‚½");

        PublicStaticStatus.ClearedChapter=PublicStaticStatus.Chapter.ChapterA;
        StartCoroutine(GoalCoroutine());

    }
    IEnumerator GoalCoroutine()
    {

        float constFloat = 2f;
        fadeAnimator.speed = 1 / constFloat;
        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(constFloat);

        SceneManager.LoadScene("StillScene01");
    }
}
