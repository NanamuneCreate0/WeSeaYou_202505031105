using UnityEngine;
public class Block : MonoBehaviour
{
    public BlockAbility ability1;
    public BlockAbility ability2;

    private void Start()
    {
        ability1?.OnStart(this);
        ability2?.OnStart(this);
    }

    private void Update()
    {
        ability1?.OnUpdate(this);
        ability2?.OnUpdate(this);
    }
}
