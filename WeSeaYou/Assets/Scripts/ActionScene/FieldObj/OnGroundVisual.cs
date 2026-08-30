using UnityEngine;

public class OnGroundVisual : MonoBehaviour
{
    private Collider2D col;
    private bool isGrounding =true;
    private bool lastIsGrounding=true;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        isGrounding = GroundUtil.CheckGrounded(
            col,
            out Collider2D col1,
            out Vector2 point);

        if (!lastIsGrounding && isGrounding)
        {
            OnGround();
        }

        lastIsGrounding = isGrounding;
    }

    void OnGround()
    {
        Debug.Log("OnG");
    }
}