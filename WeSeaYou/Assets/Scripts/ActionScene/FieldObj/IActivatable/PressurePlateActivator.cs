using System.Collections;
using UnityEngine;

public class PressurePlateActivator : MonoBehaviour//e‚ð”j‰ó‚·‚é‚æ‚¤‚É‚µ‚Ä‚éˆê’U
{
    [SerializeField]
    private MonoBehaviour activatableMonoBehaviour;
    private IActivatable _target => activatableMonoBehaviour as IActivatable;

    [SerializeField]
    private LayerMask targetLayers; 
    [SerializeField]
    private GameObject PressurePlateUpper;
    private const float MOVE_TIME = 0.3f;
    private static readonly Vector2 MOVE_AMOUNT = new Vector2(0f, -0.3f);

    private bool hasActivated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasActivated) return; 
        if (other.transform.IsChildOf(transform.parent)) return;

        if ((targetLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            hasActivated = true;
            StartCoroutine(ActivateCoroutine());
        }
    }

    private IEnumerator ActivateCoroutine()
    {
        Transform targetTransform = PressurePlateUpper.transform;

        Vector3 startPosition = targetTransform.position;
        Vector3 endPosition = startPosition + (Vector3)MOVE_AMOUNT;

        float elapsed = 0f;

        while (elapsed < MOVE_TIME)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / MOVE_TIME);
            targetTransform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        targetTransform.position = endPosition;

        _target?.Activate();

        Destroy(PressurePlateUpper);
    }
}