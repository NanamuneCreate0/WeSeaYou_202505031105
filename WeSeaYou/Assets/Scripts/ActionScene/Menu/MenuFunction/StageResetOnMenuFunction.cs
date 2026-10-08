using UnityEngine;
using UnityEngine.SceneManagement;

public class StageResetOnMenuFunction : MonoBehaviour, IMenuFunction
{
    [SerializeField]
    GameModeController_ActionScene gameModeController;
    public void Execute()
    {
        gameModeController.ChangeGameMode(GameModeController_ActionScene.GameMode.Action);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
