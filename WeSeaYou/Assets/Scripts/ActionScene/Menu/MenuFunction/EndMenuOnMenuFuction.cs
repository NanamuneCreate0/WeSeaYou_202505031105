using UnityEngine;
using static PublicStaticStatus;
using UnityEngine.SceneManagement;

public class EndMenuOnMenuFuction : MonoBehaviour, IMenuFunction
{ 
    [SerializeField ]GameModeController_ActionScene gameModeController_ActionScene;
    public void Execute()
    {
        gameModeController_ActionScene.ChangeGameMode(GameModeController_ActionScene.GameMode.Action);
    }
}
