using UnityEngine;
using UnityEngine.InputSystem;
using static ActionModeChanger;

public class SeaSkillActivator : MonoBehaviour
{
    [SerializeField] private SeaSkillExecutor seaSkillExecuter;
    [SerializeField] private SeaSkillAura seaSkillAura;
    [SerializeField] private GameObject seaSkillUI;
    [SerializeField] private ShockWaveManager _shockWaveManager;

    private void OnEnable()
    {
        ActionModeChanger.ActionModeChangeEvent += GetActionModeChange;
    }
    private void OnDisable()
    {
        ActionModeChanger.ActionModeChangeEvent -= GetActionModeChange;
    }
    void GetActionModeChange(ActionModeChanger.ActionModeType a, ActionModeChanger.ActionModeType b)
    {
        if (a == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            if (b != ActionModeChanger.ActionModeType.Neutral && b != ActionModeChanger.ActionModeType.ChikyuSkill) { Debug.LogWarning("想定外のActionMode変更"); }
            seaSkillExecuter.ActivateSkill();
            seaSkillAura.ActivateSkill();
            _shockWaveManager.StartShockWave();
            seaSkillUI.SetActive(true);
            SEManager_Nana.Instance.PlaySE(GameAssetManager.Instance.GameAssets.seaSkillSE);
        }
        if (b == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            if (a != ActionModeChanger.ActionModeType.Neutral && a != ActionModeChanger.ActionModeType.ChikyuSkill) { Debug.LogWarning("想定外のActionMode変更"); }
            seaSkillExecuter.EndSkill();
            seaSkillAura.EndSkill();
            seaSkillUI.SetActive(false);
        }

    }
}
