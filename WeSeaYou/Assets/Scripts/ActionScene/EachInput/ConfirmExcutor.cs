using System.Collections.Generic;
using UnityEngine;

public class ConfirmExecutor : MonoBehaviour//ExcuterはInput系統の命名ルールによって
{
    [SerializeField]
    private Transform playerTransfrom;
    private readonly List<ConfirmActivator> _targets = new(); 
    private ConfirmActivator _currentTarget;//nullかなり許容

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
        ConfirmActivator nearest = GetNearestTarget();
        
        if (nearest != _currentTarget)//最も近いtargetが変わった場合
        {
            _currentTarget?.Deselect();
            _currentTarget = nearest;
            _currentTarget?.Select();
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
    }
}