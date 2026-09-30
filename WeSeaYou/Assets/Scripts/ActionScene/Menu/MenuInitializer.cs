using System.Collections;
using UnityEngine;

public class MenuInitializer : MonoBehaviour//一部Initialではない
{
    [SerializeField] private GameModeController_ActionScene gameModeController;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private MenuExecutor_ActionScene excuter;


    [SerializeField] Animator menuInitializerAnim;
    bool isChangingToMenu = false;
    void OnEnable()
    {
        gameModeController.GameModeChanged += OnGameModeChanged;
    }

    void OnDisable()
    {
        gameModeController.GameModeChanged -= OnGameModeChanged;
    }

    void OnGameModeChanged(GameModeController_ActionScene.GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameModeController_ActionScene.GameMode.Menu:
                Time.timeScale = 0f;
                menuObject.SetActive(true);
                excuter.Initialize();
                break;

            default:
                Time.timeScale = 1f;
                menuObject.SetActive(false);
                break;
        }
    }

    public void StartMenu()
    {
        if (!isChangingToMenu)
        {
            isChangingToMenu = true;
            StartCoroutine(ChangeToMenu());
        }
    }
    IEnumerator ChangeToMenu()//本来スクリプト分けてもいい
    {
        gameModeController.ChangeGameMode(GameModeController_ActionScene.GameMode.Event);
        Time.timeScale = 0;
        float AnimTime = 0.07f;
        menuInitializerAnim.speed = 1 / AnimTime;
        menuObject.SetActive(true);
        //menuInitializerAnim.SetTrigger("Action");
        yield return new WaitForSecondsRealtime(AnimTime);
        gameModeController.ChangeGameMode(GameModeController_ActionScene.GameMode.Menu);
        isChangingToMenu = false;
    }
}