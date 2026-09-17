using UnityEngine;

public class PauseInitializer : MonoBehaviour//ˆê•”Initial‚Å‚Í‚È‚¢
{
    [SerializeField] private GameModeController gameModeManager;

    void OnEnable()
    {
        gameModeManager.GameModeChanged += OnGameModeChanged;
    }

    void OnDisable()
    {
        gameModeManager.GameModeChanged -= OnGameModeChanged;
    }

    void OnGameModeChanged(GameModeController.GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameModeController.GameMode.Action://‚±‚±‚ÍInitial‚Å‚Í‚È‚¢
                Time.timeScale = 1f;
                break;

            case GameModeController.GameMode.Menu:
                Time.timeScale = 0f;
                break;
        }
    }

}