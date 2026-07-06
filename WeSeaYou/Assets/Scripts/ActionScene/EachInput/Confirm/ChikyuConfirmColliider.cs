using UnityEngine;

public class ChikyuConfirmCollider : MonoBehaviour
{
    [SerializeField] private ConfirmExecutor confirmExecutor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ConfirmActivator target = other.GetComponent<ConfirmActivator>();

        if (target != null)
        {
            confirmExecutor.AddTarget(target);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ConfirmActivator target = other.GetComponent<ConfirmActivator>();

        if (target != null)
        {
            confirmExecutor.RemoveTarget(target);
        }
    }
}