using UnityEngine;
using UnityEngine.InputSystem;

public class SeaSkillManager : MonoBehaviour
{

    [SerializeField] private RepulseExecuter _repulseExecuter;
    [SerializeField] private AttractExecuter _attractExecuter;

    private ISeaSkill _seaSkill;
    private bool _isInSpecialMode = false;

    private void OnRepulsStarted(InputAction.CallbackContext _) => StartRepulslMode();
    private void OnRepulsCanceled(InputAction.CallbackContext _) => EndTrriger();
    private void OnAttractStarted(InputAction.CallbackContext _) => StartAtrractMode();
    private void OnAttractCanceled(InputAction.CallbackContext _) => EndTrriger();

    void OnEnable()
    {
        if (InputManager.Instance == null || InputManager.Instance.actions == null) return;

        var repulsAction = InputManager.Instance.actions.Player.RepulsionMode;
        var attractAction = InputManager.Instance.actions.Player.AttractionMode;

        repulsAction.started += OnRepulsStarted;
        repulsAction.canceled += OnRepulsCanceled;
        attractAction.started += OnAttractStarted;
        attractAction.canceled += OnAttractCanceled;
    }

    void OnDisable()
    {
        if (InputManager.Instance == null || InputManager.Instance.actions == null) return;

        var repulsAction = InputManager.Instance.actions.Player.RepulsionMode;
        var attractAction = InputManager.Instance.actions.Player.AttractionMode;

        repulsAction.started -= OnRepulsStarted;
        repulsAction.canceled -= OnRepulsCanceled;
        attractAction.started -= OnAttractStarted;
        attractAction.canceled -= OnAttractCanceled;
    }

    private void StartRepulslMode()
    {
        if (_isInSpecialMode) return;
        _isInSpecialMode = true;
        _seaSkill = _repulseExecuter;
        ExcecuteTrriger();
    }

    private void StartAtrractMode()
    {
        if (_isInSpecialMode) return;
        _isInSpecialMode = true;
        _seaSkill = _attractExecuter;
        ExcecuteTrriger();
    }

    private void EndTrriger()
    {
        if (_seaSkill == null) return;
        _seaSkill.End();
        _isInSpecialMode = false;
    }

    private void ExcecuteTrriger()
    {
        Debug.Log($"{_seaSkill}");
        _seaSkill.Execute();
    }

}
