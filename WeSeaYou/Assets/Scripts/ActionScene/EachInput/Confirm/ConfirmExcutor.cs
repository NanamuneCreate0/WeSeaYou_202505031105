using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfirmExecutor : MonoBehaviour//Excuter‚ÍInputŒn“‚Ì–½–¼ƒ‹[ƒ‹‚É‚æ‚Á‚Ä
{
    [SerializeField] private Transform playerTransfrom;
    [SerializeField] private ConfirmPrompt confirmPrompt;
    private readonly List<ConfirmActivator> _targets = new(); 
    private ConfirmActivator _currentTarget=null;//null‚©‚È‚è‹–—e


    [SerializeField]
    private ActionModeChanger actionModeChanger;
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
        if (b == ActionModeChanger.ActionModeType.Neutral)
        {
            InputManager.Instance.actions.Player.Decide.performed -= OnDecide;
        }
        if (a == ActionModeChanger.ActionModeType.Neutral)
        {
            InputManager.Instance.actions.Player.Decide.performed += OnDecide;
        }
    }

    private void OnDecide(InputAction.CallbackContext context)
    {
        if (actionModeChanger.ActionMode == ActionModeChanger.ActionModeType.Neutral)
        RefreshTarget();
        if (_currentTarget != null) { _currentTarget.Execute(); }
    }

    private void RefreshTarget()
    {
        _targets.RemoveAll(t => t == null);
        ConfirmActivator nearest = GetNearestTarget();

        if (nearest != _currentTarget)//Å‚à‹ß‚¢target‚ª•Ï‚í‚Á‚½ê‡
        {
            _currentTarget = nearest;
            if (_currentTarget != null)
            {
                confirmPrompt.UpdateTarget(_currentTarget);
            }
            else
            {
                confirmPrompt.ClearTarget();
            }
        }
    }
    private ConfirmActivator GetNearestTarget()
    {
        ConfirmActivator nearest = null;
        float nearestDistance = float.MaxValue;
        foreach (ConfirmActivator target in _targets)
        {
            if (target == null)
                continue;

            float distance = (target.transform.position - playerTransfrom.position).sqrMagnitude;

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = target;
            }
        }
        return nearest;
    }
    public void AddTarget(ConfirmActivator target)
    {
        if (_targets.Contains(target))
            return;

        _targets.Add(target);
    }

    public void RemoveTarget(ConfirmActivator target)
    {
        _targets.Remove(target);

        if (_currentTarget == target)
        {
            _currentTarget = null;
            confirmPrompt.ClearTarget();
        }
    }
}