using UnityEngine;

public class ChikyuConfirmCollider : MonoBehaviour
{
    [SerializeField] private ConfirmExecutor confirmExecutor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ConfirmTarget target = other.GetComponent<ConfirmTarget>();

        if (target != null)
        {
            confirmExecutor.AddTarget(target);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ConfirmTarget target = other.GetComponent<ConfirmTarget>();

        if (target != null)
        {
            confirmExecutor.RemoveTarget(target);
        }
    }
}