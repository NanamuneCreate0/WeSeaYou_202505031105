using UnityEngine;

public class DestroyOnActivate : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        Destroy(gameObject);
    }
}