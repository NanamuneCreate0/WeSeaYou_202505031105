using Unity.Collections;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    GameObject MyMainCamera;
    [SerializeField]
    ActionModeChanger controlModeChanger;
    [SerializeField]
    GameObject MyChikyu;
    [SerializeField]
    GameObject MyUtyu;

    GameObject ControlableChara;

    private Vector3 Misalignment { get { return new Vector3(0, 2f, 0); } }

    Vector3 LastPos;
    public void ModeChanged()
    {
        if (controlModeChanger.ActionMode == ActionModeChanger.ActionModeType.ChikyuView) { ControlableChara = MyChikyu; }
        if (controlModeChanger.ActionMode == ActionModeChanger.ActionModeType.UtyuView) { ControlableChara = MyUtyu; }
    }
    void Start()
    {
    }

    void Update()
    {
        //ÉJÉÅÉâí«è]
        Vector3 vec = new Vector3(ControlableChara.transform.position.x, ControlableChara.transform.position.y, -1) + Misalignment;
        MyMainCamera.transform.position = MyMainCamera.transform.position * Mathf.Pow(0.1f, Time.deltaTime) + vec * (1 - Mathf.Pow(0.1f, Time.deltaTime));
    }

}
