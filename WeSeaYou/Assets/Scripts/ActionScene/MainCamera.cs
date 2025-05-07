using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    GameObject MyMainCamera;
    [SerializeField]
    PlayerController MyPlayerController;

    const float PlayerSpeed=4;

    Vector3 LastPos;
    void Start()
    {
    }

    void Update()
    {
        //ÉJÉÅÉâí«è]
        //MyMainCamera.transform.position = new Vector3(MyPlayerController.HandlingPlayer.transform.position.x, MyPlayerController.HandlingPlayer.transform.position.y, -1);
        Vector3 vec = new Vector3(MyPlayerController.HandlingPlayer.transform.position.x, MyPlayerController.HandlingPlayer.transform.position.y, -1);
        MyMainCamera.transform.position = MyMainCamera.transform.position * Mathf.Pow(0.1f, Time.deltaTime) + vec * (1 - Mathf.Pow(0.1f, Time.deltaTime));
    }
}
