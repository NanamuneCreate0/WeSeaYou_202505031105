using UnityEngine;

public class UtyuSkillItemEventListener : MonoBehaviour
{
    [SerializeField] string targetTag = "UtyuSkillItem"; // ‘ÎÛƒ^ƒO
    [SerializeField] float requiredTime = 3f;    // ‰½•bG‚ê‚Ä‚½‚ç‹N“®‚·‚é‚©
    private float stayTime = 0f;
    private bool isTouching = false;

    private void Update()
    {
        if (isTouching)
        {
            stayTime += Time.deltaTime;

            if (stayTime >= requiredTime)
            {
                Activate();
                isTouching = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            stayTime = 0f;
            isTouching = false;
        }
    }

    private void Activate()
    {
        Debug.Log("3•bˆÈãG‚ê‚Ä‚¢‚½‚Ì‚Å‹N“®I");
    }
}
