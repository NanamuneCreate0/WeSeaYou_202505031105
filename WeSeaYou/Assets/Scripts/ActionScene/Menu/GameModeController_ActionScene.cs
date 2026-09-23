using System;
using UnityEngine;

public class GameModeController_ActionScene : MonoBehaviour
{
    public enum GameMode
    {
        Action,
        Menu
    }

    [SerializeField] private GameMode gameMode = GameMode.Action;

    public event Action<GameMode> GameModeChanged;

    void Update()
    {
        switch (gameMode)
        {
            case GameMode.Action:
                if (InputManager.Instance.actions.Player.Pause.WasPressedThisFrame())
                {
                    ChangeGameMode(GameMode.Menu);
                }
                break;

            case GameMode.Menu:
                if (InputManager.Instance.actions.UI_Nana.Pause.WasPressedThisFrame())
                {
                    ChangeGameMode(GameMode.Action);
                }
                break;
        }
    }

    public void ChangeGameMode(GameMode gm)//Input.actionsÇïœçXÇ∑ÇÈÇÃÇÕGameModeÇÃê”ñ±ÅBTimeScaleÇ‚ObjÇÃSetActiveÇïœÇ¶ÇÈÇÃÇÕê”ñ±Ç∂Ç·Ç»Ç¢ÅB
    {
        gameMode = gm;
        switch (gm)
        {
            case GameMode.Action:
                InputManager.Instance.actions.Player.Enable();
                InputManager.Instance.actions.UI_Nana.Disable();
                break;

            case GameMode.Menu:
                InputManager.Instance.actions.Player.Disable();
                InputManager.Instance.actions.UI_Nana.Enable();
                break;
        }

        GameModeChanged?.Invoke(gameMode);
    }

}