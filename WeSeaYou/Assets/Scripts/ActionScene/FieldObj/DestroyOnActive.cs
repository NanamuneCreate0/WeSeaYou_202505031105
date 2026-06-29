using UnityEngine;

public class DestroyOnActivate : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        Debug.Log("afd");
        Destroy(gameObject);
    }
}