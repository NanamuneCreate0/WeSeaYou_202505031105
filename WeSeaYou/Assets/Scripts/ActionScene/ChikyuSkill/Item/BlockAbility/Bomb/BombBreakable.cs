using UnityEngine;

public class BombBreakable : MonoBehaviour, IBombTarget
{
    public void OnBombHit()
    {
        Destroy(gameObject);
    }
}
