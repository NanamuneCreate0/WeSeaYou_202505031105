using UnityEngine;

public class FollowEvent : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    private Transform _target;
    private float _offsetX;   // 追従開始時のカメラとキャラの距離

    public void StartFollowSetting(Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning("FollowEvent: 追従対象がnullです。追従を開始しません");
            return;
        }
        _target = target;
        _offsetX = _camera.position.x - target.position.x;   // 今の距離を覚える
    }

    public void ResetFollowSetting() => _target = null;

    private void LateUpdate()
    {
        if (_target == null) return;

        var pos = _camera.position;
        pos.x = _target.position.x + _offsetX;   // 距離を保ったまま追従
        _camera.position = pos;
    }
}