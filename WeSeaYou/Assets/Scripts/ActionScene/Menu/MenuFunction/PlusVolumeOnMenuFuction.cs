using UnityEngine;
using UnityEngine.SceneManagement;

public class PlusVolumeOnMenuFuction : MonoBehaviour, IMenuFunction
{
    public void Execute()
    {
        if (PublicStaticStatus.Volume_Config >= 11)
        {
            Debug.Log("Bu");
            return;
        }

        PublicStaticStatus.Volume_Config += 1;
    }
}
