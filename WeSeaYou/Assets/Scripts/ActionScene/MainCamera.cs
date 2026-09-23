using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    GameObject MyMainCamera;

    [SerializeField]
    GameObject MyChikyu;

    [SerializeField]
    Vector3 Misalignment;

    [SerializeField]
    GameObject borderObjLeft;

    [SerializeField]
    GameObject borderObjRight;

    void Update()
    {
        // カメラ追従先
        Vector3 targetPos = new Vector3(
            MyChikyu.transform.position.x,
            MyChikyu.transform.position.y,
            -1
        ) + Misalignment;

        // カメラ中心のX移動範囲を計算
        float cameraMinX = borderObjLeft.transform.position.x + 10f;
        float cameraMaxX = borderObjRight.transform.position.x - 10f;

        // X軸だけ移動範囲内に制限
        targetPos.x = Mathf.Clamp(
            targetPos.x,
            cameraMinX,
            cameraMaxX
        );

        // カメラ追従
        MyMainCamera.transform.position = Vector3.Lerp(
            targetPos,
            MyMainCamera.transform.position,
            Mathf.Pow(0.1f, Time.deltaTime)
        );
    }
}