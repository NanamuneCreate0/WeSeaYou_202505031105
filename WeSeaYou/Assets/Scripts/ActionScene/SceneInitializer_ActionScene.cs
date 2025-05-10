using UnityEngine;

public class SceneInitializer_ActionScene: MonoBehaviour
{
    [SerializeField]
    int CurrentStage;
    void Start()
    {
        PublicStaticStatus.CurrentStage = CurrentStage;
    }
}
