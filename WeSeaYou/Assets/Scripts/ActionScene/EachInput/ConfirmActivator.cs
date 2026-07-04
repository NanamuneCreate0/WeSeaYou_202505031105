using UnityEngine;

public class ConfirmActivator : MonoBehaviour//Activator‚ÍIActivatable‹N“®‚Ì–½–¼ƒ‹[ƒ‹‚É‚æ‚Á‚Ä
{
    [SerializeField] private GameObject confirmPrompt;
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
            Debug.LogError("IActivatable‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
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
        if (confirmPrompt != null)
        {
            confirmPrompt.SetActive(visible);
        }
    }
}