using UnityEngine;

public class EnterActivator : MonoBehaviour
{
    [SerializeField] private MonoBehaviour activatableMonoBehaviour;
    private IActivatable _target;
    private void Awake()
    {
        _target = activatableMonoBehaviour as IActivatable;

        if (_target == null)
        {
            Debug.LogError($"{name}: Activatable Object must implement IActivatable.", this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _target?.Activate();
        }
    }
}
