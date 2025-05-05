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
        //�J�����Ǐ]
        MyMainCamera.transform.position = new Vector3(MyPlayerController.HandlingPlayer.transform.position.x, MyPlayerController.HandlingPlayer.transform.position.y, -1);

    }
}
