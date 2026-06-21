using UnityEngine;

public class SeaSkillTargetingObj : MonoBehaviour
{
    public Transform target;
    private void LateUpdate()
    {

        if (target)
        {
            transform.position = target.position;
        }
    }
}
