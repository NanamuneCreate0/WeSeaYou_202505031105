using DG.Tweening.Core.Easing;
using System.Collections;
using UnityEngine;

public class OnGroundSE : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;

    private bool isGrounding = true;//‰Šú‹ó’†‚Å‚àtrue¨false‚É‚È‚é‚Ì‚Å–â‘è‚È‚¢
    private bool lastIsGrounding = true;

    void Start()
    {
        col = GetComponentInChildren<Collider2D>();
        rb = col.GetComponentInParent<Rigidbody2D>();
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
        if (rb != null && rb.mass >= 7f)
        {
            //d‚¢SE‚ ‚ê‚Î
        }
        SEManager_Nana.Instance.PlaySE(GameAssetManager.Instance.GameAssets.landingSE);
    }

}