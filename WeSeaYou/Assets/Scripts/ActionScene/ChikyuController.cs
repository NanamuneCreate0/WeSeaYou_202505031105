using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class ChikyuController : MonoBehaviour
{
    public bool IsHandlingPlayer;
    public bool IsGrounding;
    [SerializeField] float moveSpeed;  //�ړ����x
    [SerializeField] float jumpPower;  //�W�����v��
    [SerializeField] float gravity;  //�W�����v��

    Rigidbody2D rb;
    IsGroundingJudger isGroundingJudger;
    Vector3 playerSpeed;
    int direction = 0;//0:�Î~//1:�E//2:��

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        isGroundingJudger = transform.GetChild(0).GetComponent<IsGroundingJudger>();
    }

    void Update()
    {

        if (IsHandlingPlayer)
        {
            //x��
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

            //Y��
            //�ڒn���Ă��鎞
            if (isGroundingJudger.IsGrounding)
            {
                playerSpeed.y = 0;
                // �W�����v
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerSpeed.y += jumpPower;
                }
            }
            // �󒆂ɂ��鎞
            if (!isGroundingJudger.IsGrounding)
            {
                // �d�͂�������
                playerSpeed.y -= gravity;
            }
        }
        if (!IsHandlingPlayer)
        {
            //Y��
            //�ڒn���Ă��鎞
            if (isGroundingJudger.IsGrounding)
            {
                playerSpeed.y = 0;
            }
            // �󒆂ɂ��鎞
            if (!isGroundingJudger.IsGrounding)
            {
                // �d�͂�������
                playerSpeed.y -= gravity;
            }
        }
        
        // �L�����N�^�[�𓮂���
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = playerSpeed;
    }
}
