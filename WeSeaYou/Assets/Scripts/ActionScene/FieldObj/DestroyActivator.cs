using UnityEngine;

public class DestroyActivator : MonoBehaviour
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

    private void OnDestroy()
    {
        _target?.Activate();
    }
}