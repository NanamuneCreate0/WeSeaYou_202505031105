using UnityEngine;
using UnityEngine.SceneManagement;

public class MinusVolumeOnMenuFunction : MonoBehaviour, IMenuFunction
{
    public void Execute()
    {
        if (PublicStaticStatus.Volume_Config <= 0)
        {
            Debug.Log("Bu");
            return;
        }

        PublicStaticStatus.Volume_Config -= 1;
    }
}
