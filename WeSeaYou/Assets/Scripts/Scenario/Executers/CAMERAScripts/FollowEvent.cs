using UnityEngine;

public class FollowEvent : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _centerTime = 0.5f; // 中心に寄るまでのおおよその時間（0なら即座）

    private Transform _target;
    private float _offsetX;
    private float _targetOffsetX;
    private float _offsetVelocity;

    // keepOffset: true なら今の距離を保ったまま追従、false なら中心に寄せる
    public void StartFollowSetting(Transform target, bool keepOffset = false)
    {
        if (target == null)
        {
            Debug.LogWarning("FollowEvent: 追従対象がnullです。追従を開始しません");
            return;
        }
        _target = target;
        _offsetX = _camera.position.x - target.position.x;
        _targetOffsetX = keepOffset ? _offsetX : 0f;
        _offsetVelocity = 0f;
    }

    public void ResetFollowSetting() => _target = null;

    private void LateUpdate()
    {
        if (_target == null) return;

        _offsetX = _centerTime > 0f
            ? Mathf.SmoothDamp(_offsetX, _targetOffsetX, ref _offsetVelocity, _centerTime)
            : _targetOffsetX;

        var pos = _camera.position;
        pos.x = _target.position.x + _offsetX;
        _camera.position = pos;
    }
}