using System;
using UnityEngine;

public class PressurePlateActivator : MonoBehaviour//e‚ð”j‰ó‚·‚é‚æ‚¤‚É‚µ‚Ä‚éˆê’U
{
    [SerializeField]
    private MonoBehaviour target;
    private IActivatable _target => target as IActivatable; [SerializeField]
    private LayerMask targetLayers;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("E");
        if (other.transform == transform.parent) return;
        if ((targetLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("PlateDes");
            _target?.Activate();
            Destroy(transform.parent != null ? transform.parent.gameObject : gameObject);
        }
    }
}
