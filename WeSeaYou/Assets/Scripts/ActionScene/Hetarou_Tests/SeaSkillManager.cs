using UnityEngine;
using UnityEngine.InputSystem;

public class SeaSkillManager : MonoBehaviour
{

    [SerializeField] private RepulseExecuter _repulseExecuter;
    [SerializeField] private AttractExecuter _attractExecuter;
    [SerializeField] private SeaSkillExecuter _seaPowerExecuter;

    private ISeaSkill _seaSkill;
    private bool _isInSpecialMode = false;

    private void OnSkillStarted(InputAction.CallbackContext _) => ModeChanger();
    //private void OnRepulsCanceled(InputAction.CallbackContext _) => EndTrriger();
    //private void OnAttractStarted(InputAction.CallbackContext _) => StartAtrractMode();
    //private void OnAttractCanceled(InputAction.CallbackContext _) => EndTrriger();

    void OnEnable()
    {
        if (InputManager.Instance == null || InputManager.Instance.actions == null) return;


        var seaAction = InputManager.Instance.actions.UI.ModeChanger;
        //var attractAction = InputManager.Instance.actions.Player.AttractionMode;

        seaAction.started += OnSkillStarted;
        //repulsAction.started += OnRepulsStarted;
        //repulsAction.canceled += OnRepulsCanceled;
        //attractAction.started += OnAttractStarted;
        //attractAction.canceled += OnAttractCanceled;
    }

    void OnDisable()
    {
        if (InputManager.Instance == null || InputManager.Instance.actions == null) return;

        var seaAction = InputManager.Instance.actions.UI.ModeChanger;
        //var attractAction = InputManager.Instance.actions.Player.AttractionMode;

        seaAction.started -= OnSkillStarted;
        //repulsAction.started -= OnRepulsStarted;
        //repulsAction.canceled -= OnRepulsCanceled;
        //attractAction.started -= OnAttractStarted;
        //attractAction.canceled -= OnAttractCanceled;
    }

    private void ModeChanger()
    {
        _isInSpecialMode = !_isInSpecialMode;

        if (_isInSpecialMode)
        {
            Debug.Log("StartSeaSkill");
            StartSkillMode();
        }
        else
        {
            Debug.Log("EndSeaSkill");
            EndTrriger();
        }
    }

    private void StartSkillMode()
    {
        //if (_isInSpecialMode) return;
        Debug.Log("”­‰Î");
        _isInSpecialMode = true;
        //_seaSkill = _repulseExecuter;
        ExcecuteTrriger();
    }

    /*private void StartAtrractMode()
    {
        if (_isInSpecialMode) return;
        _isInSpecialMode = true;
        _seaSkill = _attractExecuter;
        ExcecuteTrriger();
    }*/

    private void EndTrriger()
    {
        //if (_seaSkill == null) return;
        _seaPowerExecuter.End();
        _isInSpecialMode = false;
    }

    private void ExcecuteTrriger()
    {
        Debug.Log($"{_seaSkill}");
        _seaPowerExecuter.Execute();
    }

}
