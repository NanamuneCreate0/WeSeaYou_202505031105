using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public int ModeIControl;
    [SerializeField] float rayLength = 10f; // 最大距離
    [SerializeField] LayerMask groundLayer; // 地面のレイヤー

    void Update()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.down;

        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, groundLayer);

        if (hit.collider != null)
        {
            float distance = hit.distance;
            Debug.Log("地面までの距離: " + distance);

            // ヒットした点まで赤い線を引く
            Debug.DrawLine(origin, hit.point, Color.red);
        }
        else
        {
            // 地面に当たらなかった場合：最大距離分線を引く
            Debug.DrawLine(origin, origin + direction * rayLength, Color.red);
            Debug.Log("地面に当たらなかった");
        }
    }
}
