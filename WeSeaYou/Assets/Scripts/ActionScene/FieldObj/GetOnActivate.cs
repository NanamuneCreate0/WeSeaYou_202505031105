using UnityEngine;

public class GetOnActivate : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        Debug.Log("GetOnActivate");
        Destroy(gameObject);
    }
}
