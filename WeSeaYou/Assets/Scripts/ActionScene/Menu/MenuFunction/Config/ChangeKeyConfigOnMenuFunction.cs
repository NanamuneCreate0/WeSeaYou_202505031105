using UnityEngine;

public class ChangeKeyConfigOnMenuFunction : MonoBehaviour, IMenuFunction
{
    private enum PropertyType
    {
        SkillRoutine,
        EasyChikyuSkill,
        EasyUtyuSkill,
        ABXYSwitch
    }

    [SerializeField] private PropertyType propertyType;
    [SerializeField] private bool isActive;
    public void Execute()
    {
        switch (propertyType)
        {
            case PropertyType.SkillRoutine:
                PublicStaticStatus.SkillRoutine_Config = isActive;
                break;

            case PropertyType.EasyChikyuSkill:
                PublicStaticStatus.EasyChikyuSkill_Config = isActive;
                break;

            case PropertyType.EasyUtyuSkill:
                PublicStaticStatus.EasyUtyuSkill_Config = isActive;
                break;

            case PropertyType.ABXYSwitch:
                PublicStaticStatus.SwapABXY = isActive;
                break;
        }
    }
}
