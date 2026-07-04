using UnityEngine;

public class ConfirmActivator : MonoBehaviour
{
    [SerializeField] private MonoBehaviour activatableMonoBehaviour;

    private IActivatable _activatable;

    private void Awake()
    {
        _activatable = activatableMonoBehaviour as IActivatable;

        if (activatableMonoBehaviour != null && _activatable == null)
        {
            Debug.LogError($"{name}: İ’è‚³‚ê‚½MonoBehaviour‚ÍIActivatable‚ğÀ‘•‚µ‚Ä‚¢‚Ü‚¹‚ñB");
        }
    }

    public void Execute()
    {
        _activatable?.Activate();
    }
}