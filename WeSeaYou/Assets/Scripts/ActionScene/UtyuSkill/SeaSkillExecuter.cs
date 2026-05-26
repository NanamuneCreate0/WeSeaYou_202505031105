using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class SeaSkillExecuter : MonoBehaviour
{
    [SerializeField] private GameObject _targettingObjPrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _holdTimeLv1 = 1f;
    [SerializeField] private float _holdTimeLv2 = 2f;
    [SerializeField] private float _holdTimeLv3 = 3f;
    [SerializeField] private float _holdThreshold = 0.5f; // í∑âüÇµîªíËÇÃïbêî
    [SerializeField] private float _pullRadiusMax = 10f;
    [SerializeField] private float _pullRadiusMin = 1f;
    [SerializeField] private float _pullSpeed = 5f;
    [SerializeField] private LayerMask _targetLayer;

    private float _holdTime = 0f;
    private bool _hasJumped = false;

    private GameObject _targettingObj;
    private List<Collider2D> _hitResults = new List<Collider2D>(); 
    private List<Collider2D> _candidates = new List<Collider2D>();
    private Collider2D target;
    private ContactFilter2D _contactFilter;
    private Vector2 _currentSkillActionInput;//ÉLÅ[É{Å[ÉhÇ≈Ç¢Ç§ñÓàÛ
    private Vector2 _currentSelectSeaItemInput;
    private string targetTag;
    private bool skillActivated = false;

    void Awake()
    {
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(_targetLayer);
        _contactFilter.useLayerMask = true;
    }

    private void OnEnable()
    {
        InputManager.Instance.actions.SeaSkill.SelectRight.started += SelectRightTarget;
        InputManager.Instance.actions.SeaSkill.SelectLeft.started += SelectLeftTarget;
    }
    private void OnDisable()
    {
        InputManager.Instance.actions.SeaSkill.SelectRight.started -= SelectRightTarget;
        InputManager.Instance.actions.SeaSkill.SelectLeft.started -= SelectLeftTarget;
    }

    public void ActivateSkill()
    {
        skillActivated = true;
        _targettingObj =Instantiate(_targettingObjPrefab);
        RefreshCandidates();
        SetFirstTarget();
    }

    void FixedUpdate()
    {
        if (skillActivated)
        {
            RefreshCandidates();
            _currentSkillActionInput = InputManager.Instance.actions.Player.SeaAction.ReadValue<Vector2>();
            HandleHoldJump();
            MoveTarget(target.transform);
        }
    }
    void RefreshCandidates()
    {
        _candidates.Clear();

        int count = Physics2D.OverlapCircle(
            transform.position,
            _pullRadiusMax,
            _contactFilter,
            _hitResults);

        for (int i = 0; i < count; i++)
        {
            Collider2D col = _hitResults[i];

            if (col != null && col.CompareTag("UtyuSkillItem"))
            {
                _candidates.Add(col);
            }
        }
    }


    void SetFirstTarget()
    {
        // àÍî‘ãﬂÇ¢ÇÃtargetÇ…Ç†ÇƒÇÕÇﬂÇÈ
        target = null;
        float bestDistSqr = float.MaxValue;
        foreach (Collider2D col in _candidates)
        {
            if (col == null) continue;

            float distSqr =
                ((Vector2)col.transform.position - (Vector2)transform.position).sqrMagnitude;

            if (distSqr < bestDistSqr)
            {
                bestDistSqr = distSqr;
                target = col;
            }
        }
        SetTarget(target.transform);
    }

    void SelectRightTarget(InputAction.CallbackContext ctx)
    {
        Debug.Log("SelectRightTarget");
        // àÍî‘ãﬂÇ¢ç∂ÇíTÇ∑
        if (target == null) return;
        Collider2D best = null;
        float currentX = target.transform.position.x;
        float bestDiff = float.MaxValue;
        foreach (Collider2D col in _candidates)
        {
            if (col == null) continue;
            if (col == target) continue;

            float diff = col.transform.position.x - currentX;
            if (diff > 0f)
            {
                if (diff < bestDiff)
                {
                    bestDiff = diff;
                    best = col;
                }
            }
        }
        //targetÇ…ìñÇƒÇÕÇﬂÇÈ
        if (best != null) { target = best; }
        SetTarget(target.transform);
    }
    void SelectLeftTarget(InputAction.CallbackContext ctx)
    {
        Debug.Log("SelectLeftTarget");
        // àÍî‘ãﬂÇ¢ç∂ÇíTÇ∑
        if (target == null) return;
        Collider2D best = null;
        float currentX = target.transform.position.x;
        float bestDiff = float.MaxValue;
        foreach (Collider2D col in _candidates)
        {
            if (col == null) continue;
            if (col == target) continue;

            float diff = currentX - col.transform.position.x;

            if (diff > 0f)
            {
                if (diff < bestDiff)
                {
                    bestDiff = diff;
                    best = col;
                }
            }
        }
        //targetÇ…ìñÇƒÇÕÇﬂÇÈ
        if (best != null) { target = best; }
        SetTarget(target.transform);
    }

    private void SetTarget(Transform target)
    {
        _targettingObj.transform.position = target.position;
        _targettingObj.transform.SetParent(target);
    }


    private void HandleHoldJump()
    {
        if (_currentSkillActionInput.y > 0.5f)
        {
            _holdTime += Time.deltaTime;
            _hasJumped = true;
        }
        else if(_hasJumped)
        {
            Jump(target.transform);
            _holdTime = 0f;
            _hasJumped = false;
        }
        else
        {
            _holdTime = 0f;
            _hasJumped = false;
        }
    }

    private void Jump(Transform target)
    {
        Debug.Log("î≠âŒ");
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        float _jumpForceOrigin = _jumpForce;

        if (_holdTime < _holdTimeLv1)
        {
            _jumpForce *= 0.5f;
        }
        else if (_holdTime >= _holdTimeLv1 && _holdTime <= _holdTimeLv2)
        {
            _jumpForce *= 0.75f;
        }
        else if (_holdTime > _holdTimeLv3)
        {
            _jumpForce *= 1.5f;
        }

        Debug.Log(_jumpForce);
        rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        _jumpForce = _jumpForceOrigin;
    }

    private void MoveTarget(Transform target)
    {
        Vector2 input = InputManager.Instance.actions.Player.SeaAction.ReadValue<Vector2>();

        Vector3 moveDirection = new Vector3(input.x, 0, 0);

        target.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void End()
    {
        Destroy(_targettingObj);
        skillActivated = false;
    }
}
