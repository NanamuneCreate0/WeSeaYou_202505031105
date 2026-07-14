using UnityEngine;

public class MultiTargetActivatorOnActivate : MonoBehaviour, IActivatable
{
    [SerializeField] private MonoBehaviour[] targets;

    private IActivatable[] _activatables;

    private void Awake()
    {
        _activatables = new IActivatable[targets.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] is IActivatable activatable)
            {
                _activatables[i] = activatable;
            }
            else if (targets[i] != null)
            {
                Debug.LogError($"{targets[i].name} does not implement IActivatable.", this);
            }
        }
    }

    public void Activate()
    {
        foreach (IActivatable activatable in _activatables)
        {
            activatable?.Activate();
        }
    }
}