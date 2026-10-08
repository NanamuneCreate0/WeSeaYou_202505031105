using UnityEngine;

public class AreaInitializer : MonoBehaviour
{
    [SerializeField]
    MainCamera mainCamera;

    [SerializeField]
    SceneInitializer_ActionScene sceneInitializer;

    [SerializeField]
    GameObject borderObjLeft;

    [SerializeField]
    GameObject borderObjRight;

    [SerializeField]
    GameObject startObj;

    private void Awake()
    {
        mainCamera.SetBorderObjects(borderObjLeft, borderObjRight);
        sceneInitializer.SetStartObj(startObj);
    }
}