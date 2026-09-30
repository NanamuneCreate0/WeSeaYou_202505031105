using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] bool isGoal=false; 
    [SerializeField] Animator fadeAnimator;
    [SerializeField] private GameModeController_ActionScene gameModeController;
    public void Activate()
    {
        if (isGoal)
        {
            PublicStaticStatus.ClearedChapter++;

            Debug.Log(PublicStaticStatus.ClearedChapter + "‚ÉˆÚ‚è‚Ü‚·");

            StartCoroutine(GoalCoroutine());
        }
    }
    IEnumerator GoalCoroutine()
    {
        gameModeController.ChangeGameMode(GameModeController_ActionScene.GameMode.Event);

        float constFloat = 2f;
        fadeAnimator.speed = 1/constFloat;
        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(constFloat);

        SceneManager.LoadScene("StillScene01");
    }
}
