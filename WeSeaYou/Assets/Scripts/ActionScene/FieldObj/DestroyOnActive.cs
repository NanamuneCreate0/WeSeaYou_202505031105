using UnityEngine;

public class DestroyOnActivate : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        Debug.Log("DestroyOnActivate");
        Destroy(gameObject);
    }
}