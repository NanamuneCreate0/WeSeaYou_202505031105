using System.Collections.Generic;
using UnityEngine;

public class ConfirmExecutor : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransfrom;
    private readonly List<ConfirmTarget> _targets = new(); 
    private ConfirmTarget _currentTarget;//nullかなり許容

    private void Update()
    {
        RefreshTarget();
        if (InputManager.Instance.actions.Player.Decide.WasPressedThisFrame())
        {
            ExecuteConfirm();
        }
    }

    private void ExecuteConfirm()
    {
        if (_currentTarget != null){_currentTarget.Execute();}//_currentTarget?.Execute();は使えない。Destroy後も参照が残るため
    }
    private void RefreshTarget()
    {
        _targets.RemoveAll(t => t == null);
        ConfirmTarget nearest = GetNearestTarget();
        
        if (nearest != _currentTarget)//最も近いtargetが変わった場合
        {
            _currentTarget?.Deselect();
            _currentTarget = nearest;
            _currentTarget?.Select();
        }
    }
    private ConfirmTarget GetNearestTarget()
    {
        ConfirmTarget nearest = null;
        float nearestDistance = float.MaxValue;
        foreach (ConfirmTarget target in _targets)
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
    public void AddTarget(ConfirmTarget target)
    {
        if (_targets.Contains(target))
            return;

        _targets.Add(target);
    }

    public void RemoveTarget(ConfirmTarget target)
    {
        _targets.Remove(target);
    }
}