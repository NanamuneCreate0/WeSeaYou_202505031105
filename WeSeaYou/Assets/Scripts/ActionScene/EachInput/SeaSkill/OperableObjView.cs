using UnityEngine;

public class OperableObjView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer overlayRenderer;

    private SpriteRenderer _targetRenderer;

    private void Start()
    {
        Transform target = transform.parent;

        if (target == null)
        {
            Debug.LogError("親のOperableObjが見つかりません。", this);
            return;
        }

        _targetRenderer = target.GetComponentInChildren<SpriteRenderer>();

        if (_targetRenderer == null)
        {
            Debug.LogError("OperableObjの子にSpriteRendererが見つかりません。", this);
            return;
        }

        overlayRenderer.sprite = _targetRenderer.sprite;
    }

    public void ReflectUpdateColor(Color color)
    {
        if (overlayRenderer == null)
        {
            return;
        }

        overlayRenderer.material.SetColor("_OverlayColor", color);
    }

    private void LateUpdate()
    {
        if (_targetRenderer == null || overlayRenderer == null)
        {
            return;
        }

        overlayRenderer.sprite = _targetRenderer.sprite;
        overlayRenderer.transform.position = _targetRenderer.transform.position;
        overlayRenderer.transform.rotation = _targetRenderer.transform.rotation;
        overlayRenderer.transform.lossyScale.Equals(_targetRenderer.transform.lossyScale);
    }
}