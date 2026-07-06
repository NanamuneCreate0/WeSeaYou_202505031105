using TMPro;
using UnityEngine;

public class ConfirmPrompt : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private TMP_Text promptText;

    private ConfirmActivator _target;

    private void Awake()
    {
        // èâä˙èÛë‘ÇÕîÒï\é¶ÅiTMPìßâﬂÅj
        SetVisible(false);
    }
    private void LateUpdate()
    {
        if (_target == null)
            return;

        //transform.position = _target.transform.position + offset;
    }

    public void UpdateTarget(ConfirmActivator target)
    {
        _target = target;
        SetVisible(true);

        //transform.position = target.transform.position + offset;
    }

    public void ClearTarget()
    {
        _target = null;
        SetVisible(false);
    }
    private void SetVisible(bool visible)
    {
        if (promptText == null)
            return;

        Color c = promptText.color;
        c.a = visible ? 1f : 0f;
        promptText.color = c;
    }
}