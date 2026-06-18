using UnityEngine;

public class BombHitbox : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Bomb");
        Destroy(gameObject, 1f);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        IBombTarget target = other.GetComponent<IBombTarget>();

        if (target != null)
        {
            target.OnBombHit();
        }
    }
}
