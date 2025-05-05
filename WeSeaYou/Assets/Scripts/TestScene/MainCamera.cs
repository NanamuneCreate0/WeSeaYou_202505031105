using UnityEngine;

public class MainCamera : MonoBehaviour
{
    //public int a;//誰がaを変更するん？
    [SerializeField]
    GameObject MyMainCamera;
    [SerializeField]
    GameObject HandlingPlayer;
    [SerializeField]
    GameObject MyPlayer1;
    [SerializeField]
    GameObject MyPlayer2;



    void Start()
    {
        HandlingPlayer = MyPlayer1;
        //Debug.Log("afaaaaaaads");
    }

    void Update()
    {
        //カメラ追従
        MyMainCamera.transform.position = new Vector3(HandlingPlayer.transform.position.x, HandlingPlayer.transform.position.y, -1);

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandlingPlayer.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;//一旦球を完全に停止した
            HandlingPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 500, 0));//そのあとまた力を加えた
        }

        //左右移動
        if (Input.GetKey(KeyCode.D))
        {
            HandlingPlayer.transform.Translate(new Vector3(Time.deltaTime *2f, 0, 0));
        }

        if (Input.GetKey(KeyCode.A))
        {

            Debug.Log("afaaaaaaads");
            HandlingPlayer.transform.Translate(new Vector3(Time.deltaTime * -2f, 0, 0));
        }


        //キャラの切り替え
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("afads");
            if(HandlingPlayer==MyPlayer1)
            {
                HandlingPlayer = MyPlayer2;
            }
            else if (HandlingPlayer == MyPlayer2)
            {
                HandlingPlayer = MyPlayer1;
            }
        }
    }
}
