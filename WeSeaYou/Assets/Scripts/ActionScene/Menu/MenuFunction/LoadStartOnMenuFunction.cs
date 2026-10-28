using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartOnMenuFunction : MonoBehaviour, IMenuFunction
{
    [SerializeField]
    private GameModeController_ActionScene gameModeController;
    public void Execute()
    {
        gameModeController.ChangeGameMode(GameModeController_ActionScene.GameMode.Action, true);
        SceneManager.LoadScene("StartScene20261018");
    }
}
