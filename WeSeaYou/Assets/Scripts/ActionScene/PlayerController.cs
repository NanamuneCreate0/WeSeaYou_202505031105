using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject HandlingPlayer;
    [SerializeField]
    GameObject MyPlayer1;
    [SerializeField]
    GameObject MyPlayer2;
    const float PlayerSpeed = 4;
    void Start()
    {
        
    }

    void Update()
    {
        //キャラの切り替え
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (HandlingPlayer == MyPlayer1)
            {
                HandlingPlayer = MyPlayer2;
            }
            else if (HandlingPlayer == MyPlayer2)
            {
                HandlingPlayer = MyPlayer1;
            }
        }

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandlingPlayer.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;//一旦球を完全に停止した
            HandlingPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 500, 0));//そのあとまた力を加えた
        }

        //左右移動
        if (Input.GetKey(KeyCode.D))
        {
            HandlingPlayer.transform.Translate(new Vector3(Time.deltaTime * PlayerSpeed, 0, 0));
        }

        if (Input.GetKey(KeyCode.A))
        {
            HandlingPlayer.transform.Translate(new Vector3(Time.deltaTime * -PlayerSpeed, 0, 0));
        }

        //当たり判定けす
        //

    }
}
