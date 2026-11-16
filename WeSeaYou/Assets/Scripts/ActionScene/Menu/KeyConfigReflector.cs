using UnityEngine;

public class KeyConfigReflector : MonoBehaviour
{
    private enum PropertyType
    {
        SkillRoutine,
        EasyChikyuSkill,
        EasyUtyuSkill,
        ABXYSwitch
    }
    [SerializeField] private PropertyType propertyType;
    [SerializeField] private TMPro.TextMeshProUGUI textMeshPro;
    [SerializeField] private string trueText;
    [SerializeField] private string falseText;
    private void OnEnable()
    {
        PublicStaticStatus.ChangeKeyConfig += OnChangeKeyConfig;
    }
    private void OnDisable()
    {
        PublicStaticStatus.ChangeKeyConfig -= OnChangeKeyConfig;
    }
    private void Start()
    {
        OnChangeKeyConfig();
    }

    private void OnChangeKeyConfig()
    {

        switch (propertyType)
        {
            case PropertyType.SkillRoutine:
                textMeshPro.text = PublicStaticStatus.SkillRoutine_Config ? trueText : falseText;
                break;

            case PropertyType.EasyChikyuSkill:
                textMeshPro.text = PublicStaticStatus.EasyChikyuSkill_Config ? trueText : falseText;
                break;

            case PropertyType.EasyUtyuSkill:
                textMeshPro.text = PublicStaticStatus.EasyUtyuSkill_Config ? trueText : falseText;
                break;
            
            case PropertyType.ABXYSwitch:
                textMeshPro.text = PublicStaticStatus.SwapABXY ? trueText : falseText;
                break;
        }
    }
}
