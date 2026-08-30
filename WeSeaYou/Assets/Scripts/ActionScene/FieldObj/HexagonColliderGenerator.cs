using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class HexagonColliderGenerator : MonoBehaviour
{
    [SerializeField] private float radius = 1f;

    private PolygonCollider2D polygonCollider;

    private void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        GenerateHexagon();
    }

    private void OnValidate()
    {
        if (polygonCollider == null)
        {
            polygonCollider = GetComponent<PolygonCollider2D>();
        }

        GenerateHexagon();
    }

    private void GenerateHexagon()
    {
        if (polygonCollider == null)
        {
            return;
        }

        Vector2[] points = new Vector2[6];

        for (int i = 0; i < 6; i++)
        {
            float angle = Mathf.PI * 2f * i / 6f;

            points[i] = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );
        }

        polygonCollider.points = points;
    }
}