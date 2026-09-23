using System;
using UnityEngine;

public class PressurePlateActivator : MonoBehaviour//e‚ğ”j‰ó‚·‚é‚æ‚¤‚É‚µ‚Ä‚éˆê’U
{
    [SerializeField]
    private MonoBehaviour avtivatableMonoBehaviour;
    private IActivatable _target => avtivatableMonoBehaviour as IActivatable; [SerializeField]
    private LayerMask targetLayers;

    private void OnTriggerEnter2D(Collider2D other)//‚½‚Ü‚É•¡”‰ñ”­“®‚·‚éBŒ™‚È‚ç—vC³
    {
        if (other.transform == transform.parent) return;
        if ((targetLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            _target?.Activate();
            Destroy(transform.parent != null ? transform.parent.gameObject : gameObject);
        }
    }
}
