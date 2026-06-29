using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class SeaSkillExecuter : MonoBehaviour
{
    [SerializeField] public float SkillRadius { get; private set; } = 5f;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _targettingObjPrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask _targetLayer;
    const float _jumpForceLv0 = 8f;
    const float _jumpForceLv1 = 9f;
    const float _jumpForceLv2 = 10f;

    //Main Obj or Status
    private bool skillActivated = false;
    private Collider2D target;
    private GameObject _targetingObj;

    //Hit
    private List<Collider2D> _hitResults = new List<Collider2D>();
    private List<Collider2D> _candidates = new List<Collider2D>();
    private ContactFilter2D _contactFilter;

    //Move
    private bool IsGrounding = false;
    bool isCharging = false;
    float chargeTime;
    int chargeLevel;
    const float level1Time = 0.75f;
    const float level2Time = 1.5f;

    void Awake()
    {
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(_targetLayer);
        _contactFilter.useLayerMask = true;
    }

    private void OnEnable()
    {
        InputManager.Instance.actions.Player.SkillSelectRight.started += SelectRight;
        InputManager.Instance.actions.Player.SkillSelectLeft.started += SelectLeft;
    }
    private void OnDisable()
    {
        InputManager.Instance.actions.Player.SkillSelectRight.started -= SelectRight;
        InputManager.Instance.actions.Player.SkillSelectLeft.started -= SelectLeft;
    }

    public void ActivateSkill()
    {
        skillActivated = true;
        _targetingObj = Instantiate(_targettingObjPrefab);
        RefreshCandidates();
        //EndSkillの権限はAciivatorなので、変えたいなら、Aciivatorが変わるように、ActionModeごと変えないとダメ
        //if (_candidates.Count == 0) { EndSkill(); }
        //else { SetFirstTarget(); }
        SetFirstTarget();
    }
    public void EndSkill()
    {
        skillActivated = false;
        Destroy(_targetingObj);
    }

    void FixedUpdate()
    {
        if (!skillActivated) { return; }

        //範囲捜索
        RefreshCandidates();

        //targetが範囲外
        if (!_candidates.Contains(target))
        {
            target = null;
            SetTarget(null);
        }

        //targetが無くて新たなtargetを得る
        if (target == null && _candidates.Count != 0)
        {
            SetFirstTarget();
        }

        //移動
        if (target != null)
        {
            HandleJump();
            HandleMove(target.transform);
        }
    }
    void SetFirstTarget()
    {
        // 一番近いのtargetにあてはめる
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
        SetTarget(target != null ? target.transform : null);
    }

    void SelectRight(InputAction.CallbackContext ctx)
    {
        if (!skillActivated) { return; }
        RefreshCandidates();
        Debug.Log("SelectRightTarget");
        // 一番近い左を探す
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
        //targetに当てはめる
        if (best != null) { target = best; }
        SetTarget(target.transform);
    }
    void SelectLeft(InputAction.CallbackContext ctx)
    {
        if (!skillActivated) { return; }
        RefreshCandidates();
        Debug.Log("SelectLeftTarget");
        // 一番近い左を探す
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
        //targetに当てはめる
        if (best != null) { target = best; }
        SetTarget(target.transform);
    }

    void RefreshCandidates()
    {
        _candidates.Clear();

        int count = Physics2D.OverlapCircle(
            _player.transform.position,
            SkillRadius,
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
        //Debug.Log(_candidates.Count);
    }

    private void SetTarget(Transform target)
    {

        isCharging = false;
        chargeTime = 0f;
        chargeLevel = 0;
        if (_targetingObj != null) _targetingObj.GetComponent<SpriteRenderer>().color = Color.blue;
        if (target == null)
        {
            _targetingObj.SetActive(false);
        }
        else
        {
            _targetingObj.SetActive(true);
            _targetingObj.GetComponent<SeaSkillTargetingObj>().target = target;
        }
    }


    private void HandleJump()
    {
        Vector2 input = InputManager.Instance.actions.Player.SeaSkillMove.ReadValue<Vector2>();
        IsGrounding = GroundUtil.CheckGrounded(
            target,
            out Collider2D col,
            out Vector2 point);

        if (!IsGrounding)
        {
            isCharging = false;
            chargeTime = 0f;
            return;
        }
        //else { Debug.Log("land"); }

        bool downInput = input.y < -0.5f;

        // 押し始め
        if (downInput && !isCharging)
        {
            isCharging = true;
            chargeTime = 0f;
        }

        // 溜め中
        if (downInput && isCharging)
        {
            chargeTime += Time.deltaTime;

            if (chargeTime >= level2Time)
            {
                chargeLevel = 2;
                _targetingObj.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (chargeTime >= level1Time)
            {
                chargeLevel = 1;
                _targetingObj.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else
            {
                chargeLevel = 0;
            }
        }

        // 離した
        if (!downInput && isCharging)
        {
            Debug.Log($"Jump Level : {chargeLevel}");
            float jumpForce = chargeLevel switch
            {
                2 => _jumpForceLv2,
                1 => _jumpForceLv1,
                _ => _jumpForceLv0
            };

            isCharging = false;
            chargeTime = 0f;
            chargeLevel = 0;
            _targetingObj.GetComponent<SpriteRenderer>().color = Color.blue;

            target.GetComponent<Rigidbody2D>()
                .AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
    }

    private void HandleMove(Transform target)
    {
        if (isCharging) { return; }
        Vector2 input = InputManager.Instance.actions.Player.SeaSkillMove.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(input.x, 0, 0);
        target.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

}
