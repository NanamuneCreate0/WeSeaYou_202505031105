using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnMenuFunction : MonoBehaviour, IMenuFunction
{
    [SerializeField]
    private GameModeController_ActionScene gameModeController;
    [SerializeField]
    private PublicStaticStatus.Chapter chapter;
    public void Execute()
    {
        gameModeController.ChangeGameMode(GameModeController_ActionScene.GameMode.Action);
        PublicStaticStatus.ClearedChapter=chapter;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
