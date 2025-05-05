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
        //�L�����̐؂�ւ�
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

        //�W�����v
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandlingPlayer.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;//��U�������S�ɒ�~����
            HandlingPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 500, 0));//���̂��Ƃ܂��͂�������
        }

        //���E�ړ�
        if (Input.GetKey(KeyCode.D))
        {
            HandlingPlayer.transform.Translate(new Vector3(Time.deltaTime * PlayerSpeed, 0, 0));
        }

        if (Input.GetKey(KeyCode.A))
        {
            HandlingPlayer.transform.Translate(new Vector3(Time.deltaTime * -PlayerSpeed, 0, 0));
        }

        //�����蔻�肯��
        //

    }
}
