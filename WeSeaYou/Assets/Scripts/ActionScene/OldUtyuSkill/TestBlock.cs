using UnityEngine;

public class TestBlock : MonoBehaviour, UtyuSkillEventInterface
{
    public void OnActivate()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
