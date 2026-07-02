using UnityEngine;

public class SeaSkillAura : MonoBehaviour
{
    [SerializeField] GameObject seaSkillAuraObj;
    [SerializeField] SeaSkillExecutor _seaSkillExcuter;
    [SerializeField] Transform _chikyuParent;

    GameObject go;
    public void ActivateSkill()
    {
        go=Instantiate(seaSkillAuraObj, _chikyuParent);
        go.transform.localScale = new Vector3(2*_seaSkillExcuter.SkillRadius, 2*_seaSkillExcuter.SkillRadius, 1);
    }
    public void EndSkill()
    {
        Destroy(go);
    }
}
