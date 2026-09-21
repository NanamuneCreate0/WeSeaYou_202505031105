using UnityEngine;

public static class GroundUtil
{
    const float RAY_LENGTH = 0.2f;
    public static bool CheckGrounded(
        Collider2D col,
        out Collider2D groundCol,
        out Vector2 hitPoint)
    {
        groundCol = null;
        hitPoint = Vector2.zero;

        Bounds bounds = col.bounds;

        // 下端（足元）
        float footY = bounds.min.y-0.03f;//EdgeRadiusより大きく

        // 左・中央・右
        Vector2[] origins = new Vector2[]
        {
            new Vector2(bounds.min.x, footY),
            new Vector2(bounds.center.x, footY),
            new Vector2(bounds.max.x, footY)
        };


        int mask = LayerMask.GetMask("Ground", "Block");
        foreach (var origin in origins)
        {
            //int mask = ~LayerMask.GetMask("Player");
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, RAY_LENGTH, mask);

            Debug.DrawRay(origin, Vector2.down * RAY_LENGTH, Color.red);

            if (hit.collider != null)
            {
                if (hit.collider.attachedRigidbody != null)
                {
                    groundCol = hit.collider;
                    hitPoint = hit.point;
                    return true;
                }
            }
        }

        return false;
    }
}