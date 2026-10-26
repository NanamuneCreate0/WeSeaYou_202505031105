using UnityEngine;

public class SceneFinalizer_ActionScene : MonoBehaviour
{
    private void OnDestroy()
    {
        PublicStaticItem.ClearStageItem();
    }
}
