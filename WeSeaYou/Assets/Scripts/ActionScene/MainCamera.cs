using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    GameObject MyMainCamera;
    [SerializeField]
    PlayerController MyPlayerController;

    const float PlayerSpeed=4;



    void Start()
    {
    }

    void Update()
    {
        //ÉJÉÅÉâí«è]
        MyMainCamera.transform.position = new Vector3(MyPlayerController.HandlingPlayer.transform.position.x, MyPlayerController.HandlingPlayer.transform.position.y, -1);

    }
}
