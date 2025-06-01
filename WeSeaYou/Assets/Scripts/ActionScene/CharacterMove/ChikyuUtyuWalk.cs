using System;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class ChikyuUtyuWalk : MonoBehaviour
{
    [SerializeField] string AnotherWalkComponentName;
    [SerializeField] float moveSpeed;  //移動速度
    [SerializeField] float jumpPower;  //ジャンプ力
    [SerializeField] GameObject MyAnother;
    [SerializeField] Animator MyAnimator;
    public bool IsHandlingPlayer;

    const float CloseDistance = 1f;
    const float BigCloseDistance = 4f;
    const float ConfortDistance = 3;
    const float MaxConfortDistance = 6;

    Rigidbody2D rb;
    IsGroundingJudger isGroundingJudger;
    Vector3 playerSpeed;
    int direction = 0;//0:静止//1:右//2:左
    string LastAnim;

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
                MyAnimator.SetTrigger("WalkRight");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = 2;
                MyAnimator.SetTrigger("WalkLeft");
            }
            if (Input.GetKeyUp(KeyCode.D) && direction == 1)
            {
                direction = 0; MyAnimator.SetTrigger("StandRight"); if (Input.GetKey(KeyCode.A)) { direction = 2; MyAnimator.SetTrigger("WalkLeft"); }
            }
            if (Input.GetKeyUp(KeyCode.A) && direction == 2)
            {
                direction = 0; MyAnimator.SetTrigger("StandLeft"); if (Input.GetKey(KeyCode.D)) { direction = 1; MyAnimator.SetTrigger("WalkRight"); }
            }
            if (direction == 0) { playerSpeed.x = 0; }

            //スピード判定
            //if (direction == 1) { playerSpeed.x = moveSpeed; }
            //if (direction == 2) { playerSpeed.x = -moveSpeed; }
            float num0 = Vector3.Distance(transform.position, MyAnother.transform.position);
            if (direction == 1)
            {
                if (transform.position.x - MyAnother.transform.position.x < 0)
                {
                    playerSpeed.x = moveSpeed;
                }
                else if (transform.position.x - MyAnother.transform.position.x >= 0)
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
                if (transform.position.x - MyAnother.transform.position.x > 0)
                {
                    playerSpeed.x = -moveSpeed;
                }
                else if (transform.position.x - MyAnother.transform.position.x <= 0)
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
            MyAnimator.SetFloat("AnimSpeed", MathF.Abs(playerSpeed.x / moveSpeed));

            // ジャンプ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.IsHandling_Jump();
                //MyAnother.GetComponent<AnotherWalk>().IsNotHandling_Jump();
                MyAnother.GetComponent(Type.GetType(AnotherWalkComponentName)).SendMessage("IsNotHandling_Jump");
            }
        }
        if (!IsHandlingPlayer)
        {
            //playerSpeed.x = Mathf.Sign(MyAnother.transform.position.x - transform.position.x) * moveSpeed;
            //rb.linearVelocityX = playerSpeed.x;
            float num1 = Vector3.Distance(transform.position, MyAnother.transform.position);
            if (CloseDistance >= num1)
            {
                playerSpeed.x = 0;
            }
            else if (CloseDistance < num1 && num1 < BigCloseDistance)
            {
                playerSpeed.x = Mathf.Sign(MyAnother.transform.position.x - transform.position.x)
                    * moveSpeed * Mathf.Pow((num1 - CloseDistance) / (BigCloseDistance - CloseDistance), 0.5f);
            }
            else if (BigCloseDistance <= num1)
            {
                playerSpeed.x = Mathf.Sign(MyAnother.transform.position.x - transform.position.x) * moveSpeed;
            }

            rb.linearVelocityX = playerSpeed.x;
            Debug.Log(playerSpeed.x);
            if (playerSpeed.x<0.2)
            {
                if(MyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name=="WalkLeft")
                {
                    MyAnimator.SetTrigger("StandRight");
                }
                else
                {
                    MyAnimator.SetTrigger("StandRight");
                }
            }
            else if (MathF.Sign(playerSpeed.x) == 1)
            {
                MyAnimator.SetTrigger("WalkRight");
            }
            else if (MathF.Sign(playerSpeed.x) == -1)
            {
                MyAnimator.SetTrigger("WalkLeft");
            }
            MyAnimator.SetFloat("AnimSpeed", MathF.Abs(playerSpeed.x / moveSpeed));
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
