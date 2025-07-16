using Unity.Collections;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    GameObject MyMainCamera;
    [SerializeField]
    PlayerSwitcher MyPlayerSwitcher;
    [SerializeField]
    GameObject MyChikyu;
    [SerializeField]
    GameObject MyUtyu;

    GameObject ControlableChara;

    private Vector3 Misalignment { get { return new Vector3(0, 2f, 0); } }

    Vector3 LastPos;
    void Start()
    {
    }

    void Update()
    {
        //ÉJÉÅÉâí«è]
        //MyMainCamera.transform.position = new Vector3(MyPlayerController.HandlingPlayer.transform.position.x, MyPlayerController.HandlingPlayer.transform.position.y, -1);
        Vector3 vec = new Vector3(ControlableChara.transform.position.x, ControlableChara.transform.position.y, -1) + Misalignment;
        MyMainCamera.transform.position = MyMainCamera.transform.position * Mathf.Pow(0.1f, Time.deltaTime) + vec * (1 - Mathf.Pow(0.1f, Time.deltaTime));
    }

    public void ModeChanged()
    {
        if (MyPlayerSwitcher.ControlMode == 0) { ControlableChara = MyChikyu; }
        if (MyPlayerSwitcher.ControlMode == 1) { ControlableChara = MyUtyu; }
    }
}
