using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class UtyuWalk : MonoBehaviour
{
    public bool IsHandlingPlayer;
    public bool IsGrounding;
    [SerializeField] float moveSpeed;  //移動速度
    [SerializeField] float jumpPower;  //ジャンプ力
    [SerializeField] GameObject MyChikyu;
    [SerializeField] Animator MyAnimator;
    const float CloseDistance = 1f;
    const float BigCloseDistance = 4f;
    const float ConfortDistance = 3;
    const float MaxConfortDistance = 6;

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
            //if (direction == 1) { playerSpeed.x = moveSpeed; }
            //if (direction == 2) { playerSpeed.x = -moveSpeed; }
            float num0 = Vector3.Distance(transform.position, MyChikyu.transform.position);
            if (direction == 1)
            {
                if (transform.position.x - MyChikyu.transform.position.x < 0)
                {
                    playerSpeed.x = moveSpeed;
                }
                else if (transform.position.x - MyChikyu.transform.position.x >= 0)
                {
                    if (3 >= num0)
                    {
                        playerSpeed.x = moveSpeed;
                    }
                    if (3 < num0 && num0 < MaxConfortDistance)
                    {
                        playerSpeed.x = moveSpeed * Mathf.Pow((MaxConfortDistance - num0) / (MaxConfortDistance - ConfortDistance), 2);
                    }
                    if (MaxConfortDistance <= num0)
                    {
                        playerSpeed.x = 0;
                    }
                }
            }
            if (direction == 2)
            {
                if (transform.position.x - MyChikyu.transform.position.x > 0)
                {
                    playerSpeed.x = -moveSpeed;
                }
                else if (transform.position.x - MyChikyu.transform.position.x <= 0)
                {
                    if (3 >= num0)
                    {
                        playerSpeed.x = -moveSpeed;
                    }
                    if (3 < num0 && num0 < MaxConfortDistance)
                    {
                        playerSpeed.x = -moveSpeed * Mathf.Pow((MaxConfortDistance - num0) / (MaxConfortDistance - ConfortDistance), 2);
                    }
                    if (MaxConfortDistance <= num0)
                    {
                        playerSpeed.x = 0;
                    }
                }
            }
            // キャラクターを動かす
            rb.linearVelocityX = playerSpeed.x;

            // ジャンプ
            if (Input.GetKeyDown(KeyCode.Space))
            {

                this.IsHandling_Jump();
                MyChikyu.GetComponent<ChikyuWalk>().IsNotHandling_Jump();

            }
        }
        if (!IsHandlingPlayer)
        {
            //playerSpeed.x = Mathf.Sign(MyChikyu.transform.position.x - transform.position.x) * moveSpeed;
            //rb.linearVelocityX = playerSpeed.x;
            //rb.linearVelocityX = moveSpeed * (MyChikyu.transform.position.x - transform.position.x);
            float num1 = Vector3.Distance(transform.position, MyChikyu.transform.position);
            if (CloseDistance >= num1)
            {
                playerSpeed.x = 0;
            }
            if (CloseDistance < num1 && num1 < BigCloseDistance)
            {
                playerSpeed.x = Mathf.Sign(MyChikyu.transform.position.x - transform.position.x) * moveSpeed 
                    * Mathf.Pow((num1 - CloseDistance) / (BigCloseDistance - CloseDistance), 0.3f);//0.5fが1fだとバネと同じ、0だと等速
            }
            if (BigCloseDistance <= num1)
            {
                playerSpeed.x = Mathf.Sign(MyChikyu.transform.position.x - transform.position.x) * moveSpeed;
            }
            rb.linearVelocityX = playerSpeed.x;
        }

    }

    public void IsHandling_Jump()
    {
        if (isGroundingJudger.IsGrounding)
        {
            rb.AddForce(new Vector3(0, jumpPower * 20, 0));
        }
    }
    public void IsNotHandling_Jump()
    {
        if (isGroundingJudger.IsGrounding)
        {
            rb.AddForce(new Vector3(0, jumpPower * 20, 0));
        }
    }
}
