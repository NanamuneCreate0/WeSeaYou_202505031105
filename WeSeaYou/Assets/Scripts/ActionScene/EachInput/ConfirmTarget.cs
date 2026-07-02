using UnityEngine;

public class ConfirmTarget : MonoBehaviour
{
    [Header("Prompt")]
    [SerializeField] private GameObject promptObject;

    [Header("Activate")]
    [SerializeField] private MonoBehaviour activatableMonoBehaviour;

    private IActivatable _activatable;

    private void Awake()
    {
        _activatable = activatableMonoBehaviour as IActivatable;
        SetPromptVisible(false);
    }
    private void OnDisable()
    {
        SetPromptVisible(false);
    }

    public void Execute()
    {
        if (_activatable == null)
        {
            Debug.LogError("IActivatableÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
            return;
        }
        _activatable.Activate();
    }
    public void Select()
    {
        SetPromptVisible(true);
    }

    public void Deselect()
    {
        SetPromptVisible(false);
    }
    private void SetPromptVisible(bool visible)
    {
        if (promptObject != null)
        {
            promptObject.SetActive(visible);
        }
    }
}