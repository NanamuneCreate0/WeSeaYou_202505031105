using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class ChikyuController : MonoBehaviour
{
    public bool IsHandlingPlayer;
    public bool IsGrounding;
    [SerializeField] float moveSpeed;  //移動速度
    [SerializeField] float jumpPower;  //ジャンプ力
    [SerializeField] float gravity;  //ジャンプ力

    Rigidbody2D rb;
    IsGroundingJudger isGroundingJudger;
    Vector3 playerSpeed;
    int direction = 0;//0:静止//1:右//2:左

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        isGroundingJudger = transform.GetChild(0).GetComponent<IsGroundingJudger>();
    }

    void Update()
    {

        if (IsHandlingPlayer)
        {
            //x軸
            if (Input.GetKeyDown(KeyCode.D))
            {
                direction = 1;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = 2;
            }
            if (Input.GetKeyUp(KeyCode.D) && direction == 1)
            {
                direction = 0; if (Input.GetKey(KeyCode.A)) { direction = 2; }
            }
            if (Input.GetKeyUp(KeyCode.A) && direction == 2)
            {
                direction = 0; if (Input.GetKey(KeyCode.D)) { direction = 1; }
            }
            if (direction == 0) { playerSpeed.x = 0; }
            if (direction == 1) { playerSpeed.x = moveSpeed; }
            if (direction == 2) { playerSpeed.x = -moveSpeed; }

            //Y軸
            //接地している時
            if (isGroundingJudger.IsGrounding)
            {
                playerSpeed.y = 0;
                // ジャンプ
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerSpeed.y += jumpPower;
                }
            }
            // 空中にいる時
            if (!isGroundingJudger.IsGrounding)
            {
                // 重力をかける
                playerSpeed.y -= gravity;
            }
        }
        if (!IsHandlingPlayer)
        {
            //Y軸
            //接地している時
            if (isGroundingJudger.IsGrounding)
            {
                playerSpeed.y = 0;
            }
            // 空中にいる時
            if (!isGroundingJudger.IsGrounding)
            {
                // 重力をかける
                playerSpeed.y -= gravity;
            }
        }
        
        // キャラクターを動かす
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = playerSpeed;
    }
}
