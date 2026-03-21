using UnityEngine;
using static UnityEngine.UI.Image;

public class WallJudger_Raycast : MonoBehaviour
{/*
    [SerializeField] LayerMask groundLayer;
    Vector2 direction;
    Vector2 instantOrigin;
    ChikyuUtyuWalk MyWalk;

    void Start()
    {
        CapsuleCollider2D capsule = transform.parent.GetComponent<CapsuleCollider2D>();
        MyWalk=transform.parent.GetComponent<ChikyuUtyuWalk>();

        //ローカル座標を1マス目中心にする
        float localBottom = capsule.offset.y - capsule.size.y / 2f;
        transform.localPosition = new Vector2(0,localBottom)+ new Vector2(0, 0.5f);
    }
    void Update()
    {
        if (MyWalk.direction == 1) { direction = Vector2.right; }
        if (MyWalk.direction == 2) { direction = Vector2.left; }
        instantOrigin = (Vector2)transform.position;
        //Debug.Log("FrontIsWall:" + FrontIsWall(instantOrigin, Vector2.right));
        if(FrontIsWall(instantOrigin, direction))
        {
            instantOrigin += new Vector2(0, 1);
            Debug.Log("+1,FrontIsEmpty:" + FrontIsEmpty(instantOrigin, direction));
            if(FrontIsEmpty(instantOrigin, direction))
            {
                Debug.Log("Excute1up");
                transform.parent.Translate(direction * 1 + new Vector2(0, 1));
            }
            else
            {
                instantOrigin += new Vector2(0, 1);
                Debug.Log("+2,FrontIsEmpty:" + FrontIsEmpty(instantOrigin, Vector2.right));
                if (FrontIsEmpty(instantOrigin, Vector2.right))
                {
                    Debug.Log("Excute2up");
                    transform.parent.Translate(direction * 1 + new Vector2(0, 2));
                }
                else
                {
                    instantOrigin += new Vector2(0, 1);
                    Debug.Log("+2,FrontIsEmpty:" + FrontIsEmpty(instantOrigin, Vector2.right));
                    if (FrontIsEmpty(instantOrigin, Vector2.right))
                    {
                        Debug.Log("Excute3up");
                        transform.parent.Translate(direction * 1 + new Vector2(0, 3));
                    }
                    else
                    {
                        Debug.Log("more than 3");
                    }
                }
            }
        }
    }
    bool FrontIsWall(Vector2 origin, Vector2 direction)
    {
        float rayLength = 0.6f;
        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, groundLayer);
        if (hit.collider != null)
        {
            //Debug.Log("壁までの距離: " + hit.distance);
            Debug.DrawLine(origin, hit.point, Color.red);
            return (true);
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * rayLength, Color.red);
            return (false);
        }
    }
    bool FrontIsEmpty(Vector2 origin, Vector2 direction)
    {
        float rayLength = 1f;
        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, groundLayer);
        if (hit.collider != null)
        {
            //Debug.Log("壁までの距離: " + hit.distance);
            Debug.DrawLine(origin, hit.point, Color.red);
            return (false);
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * rayLength, Color.red);
            return (true);
        }
    }*/
}
