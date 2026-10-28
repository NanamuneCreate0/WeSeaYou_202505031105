using System;
using System.Collections;
using UnityEngine;

public class GameModeController_ActionScene : MonoBehaviour
{
    public enum GameMode
    {
        Action,
        Menu,
        Event
    }

    [SerializeField] private GameMode gameMode = GameMode.Action;

    public event Action<GameMode> GameModeChanged;

    [SerializeField] private MenuInitializer menuInitializer;
    void Update()
    {
        switch (gameMode)
        {
            case GameMode.Action:
                if (InputManager.Instance.actions.Player.Pause.WasPressedThisFrame())
                {
                    menuInitializer.StartMenu();
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
    public void ChangeGameMode(GameMode gm, bool executeImmediately = false)//InputSystemのPerformedを二重誘発させないため1f待つ//他シーンを呼ぶなら即実行
    {
        if (executeImmediately)
        {
            ChangeGameModeImmediate(gm);
        }
        else
        {
            StartCoroutine(ChangeGameModeCoroutine(gm));
        }
    }

    private IEnumerator ChangeGameModeCoroutine(GameMode gm)
    {
        yield return null;

        ChangeGameModeImmediate(gm);
    }

    private void ChangeGameModeImmediate(GameMode gm)
    {
        // Input.actionsを変更するのはGameModeの責務
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

            case GameMode.Event:
                InputManager.Instance.actions.Player.Disable();
                InputManager.Instance.actions.UI_Nana.Disable();
                break;
        }

        GameModeChanged?.Invoke(gameMode);
    }
}