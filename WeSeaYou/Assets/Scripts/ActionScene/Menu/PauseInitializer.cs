using UnityEngine;

public class PauseInitializer : MonoBehaviour//ˆê•”Initial‚Å‚Í‚È‚¢
{
    [SerializeField] private GameModeController_ActionScene gameModeController;
    [SerializeField] private GameObject menuObject;

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
            case GameModeController_ActionScene.GameMode.Action://‚±‚±‚ÍInitial‚Å‚Í‚È‚¢
                Time.timeScale = 1f;
                menuObject.SetActive(false);
                break;

            case GameModeController_ActionScene.GameMode.Menu:
                Time.timeScale = 0f;
                menuObject.SetActive(true);
                break;
        }
    }

}