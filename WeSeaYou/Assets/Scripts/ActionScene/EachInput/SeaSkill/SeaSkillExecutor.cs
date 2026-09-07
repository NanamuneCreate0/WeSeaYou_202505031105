using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeaSkillExecutor : MonoBehaviour
{
    public event Action<Collider2D, bool> CandidateStateChanged;
    public event Action<Collider2D, bool> TargetStateChanged;
    [SerializeField] public float SkillRadius { get; private set; } = 5f;
    [SerializeField] private GameObject _player;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask _targetLayer;
    const float _jumpForceLv0 = 8f;
    const float _jumpForceLv1 = 9f;
    const float _jumpForceLv2 = 10f;

    //Main Obj or Status
    private bool skillActivated = false;
    private Collider2D target;

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
        RefreshCandidates();
        //EndSkillの権限はAciivatorなので、変えたいなら、Aciivatorが変わるように、ActionModeごと変えないとダメ
        //if (_candidates.Count == 0) { EndSkill(); }
        //else { SetFirstTarget(); }
        SetFirstTarget();
    }
    public void EndSkill()
    {

        SetTarget(null);

        //ClearCandidates()
        foreach (Collider2D candidate in _candidates)
        {
            if (candidate != null)
            {
                CandidateStateChanged?.Invoke(candidate, false);
            }
        }
        _candidates.Clear();

        skillActivated = false;
    }

    void FixedUpdate()
    {
        if (!skillActivated) { return; }

        //範囲捜索
        RefreshCandidates();

        //Targetの更新
        RefreshTarget();

        //移動
        if (target != null)
        {
            HandleJump();
            HandleMove(target.transform);
        }
    }
    void RefreshTarget()
    {
        //targetが範囲外
        if (!_candidates.Contains(target))
        {
            SetTarget(null);
        }

        //targetが無くて新たなtargetを得る
        if (target == null && _candidates.Count != 0)
        {
            SetFirstTarget();
        }
    }
    void SetFirstTarget()
    {
        // 一番近いTargetを探す
        Collider2D best = null;
        float bestDistSqr = float.MaxValue;

        foreach (Collider2D col in _candidates)
        {
            if (col == null) continue;

            float distSqr =
                ((Vector2)col.transform.position - (Vector2)transform.position).sqrMagnitude;

            if (distSqr < bestDistSqr)
            {
                bestDistSqr = distSqr;
                best = col;
            }
        }

        SetTarget(best);
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
        if (best != null)
        {
            SetTarget(best);
        }
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
        if (best != null)
        {
            SetTarget(best);
        }
    }

    /*void RefreshCandidates()
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
    }*/
    /*void RefreshCandidates()
    {
        // 前回の候補を保存
        List<Collider2D> previousCandidates = new List<Collider2D>(_candidates);

        _candidates.Clear();

        int count = Physics2D.OverlapCircle(
            _player.transform.position,
            SkillRadius,
            _contactFilter,
            _hitResults);

        for (int i = 0; i < count; i++)
        {
            Collider2D col = _hitResults[i];

            if (col == null || !col.CompareTag("UtyuSkillItem"))
                continue;
            _candidates.Add(col);
            // 新しく候補になった
            if (!previousCandidates.Contains(col))
            {
                if (col.TryGetComponent<IOperable>(out var operable))
                {
                    operable.BecomeCandidateColor();
                }
                else
                {
                    Debug.LogWarning("CandidatesMustHanveIOperable");
                }
            }
        }

        // 前回は候補だったが、今回は候補ではなくなった
        foreach (Collider2D col in previousCandidates)
        {
            if (!_candidates.Contains(col) &&
                col != null )
            {
                if (col.TryGetComponent<IOperable>(out var operable))
                {
                    operable.ResetColor();
                }
                else
                {
                    Debug.LogWarning("CandidatesMustHanveIOperable");
                }
            }
        }
    }*/
    /*private void RefreshCandidates()
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

            if (col == null || !col.CompareTag("UtyuSkillItem"))
                continue;

            _candidates.Add(col);
        }

        CandidateStateChanged?.Invoke(_candidates);
    }*/
    private void RefreshCandidates()
    {
        List<Collider2D> previousCandidates = new List<Collider2D>(_candidates);

        _candidates.Clear();

        int count = Physics2D.OverlapCircle(
            _player.transform.position,
            SkillRadius,
            _contactFilter,
            _hitResults);

        for (int i = 0; i < count; i++)
        {
            Collider2D col = _hitResults[i];

            if (col == null || !col.CompareTag("UtyuSkillItem"))
                continue;

            _candidates.Add(col);

            // 新しくCandidateになった
            if (!previousCandidates.Contains(col))
            {
                CandidateStateChanged?.Invoke(col, true);
            }
        }

        // Candidateではなくなった
        foreach (Collider2D col in previousCandidates)
        {
            if (col != null && !_candidates.Contains(col))
            {
                CandidateStateChanged?.Invoke(col, false);
            }
        }
    }
    private void SetTarget(Collider2D newTarget)
    {
        isCharging = false;
        chargeTime = 0f;
        chargeLevel = 0;

        if (target != newTarget)
        {
            if (target != null)
            {
                TargetStateChanged?.Invoke(target, false);
            }

            target = newTarget;

            if (target != null)
            {
                TargetStateChanged?.Invoke(target, true);
            }
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
                /////////////色を変える赤
                target.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (chargeTime >= level1Time)
            {
                chargeLevel = 1;
                /////////////色を変える黄色
                target.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                chargeLevel = 0;
            }
        }

        // 離した
        if (!downInput && isCharging)
        {
            //Debug.Log($"Jump Level : {chargeLevel}");
            float jumpForce = chargeLevel switch
            {
                2 => _jumpForceLv2,
                1 => _jumpForceLv1,
                _ => _jumpForceLv0
            };

            isCharging = false;
            chargeTime = 0f;
            chargeLevel = 0;
            /////////////色を変える青
            target.GetComponent<SpriteRenderer>().color = Color.yellow;

            target.GetComponent<Rigidbody2D>()
                .AddForce(Vector2.up * jumpForce*target.GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse);

        }
    }
    private void HandleMove(Transform target)
    {
        if (isCharging) { return; }

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb == null) { return; }

        Vector2 input = InputManager.Instance.actions.Player.SeaSkillMove.ReadValue<Vector2>();

        
        float groundVelocityX = 0f;
        if (GroundUtil.CheckGrounded(
            target.GetComponent<Collider2D>(),
            out Collider2D groundCol,
            out Vector2 hitPoint))
        {
            IVelocityProvider provider = groundCol.GetComponent<IVelocityProvider>();
            if (provider != null)
            {
                groundVelocityX = provider.Velocity.x;
                Debug.Log(provider);
            }
        }

        if (Mathf.Abs(input.x) > 0.4f)
        {
            rb.linearVelocity = new Vector2(
                Mathf.Sin(input.x) * moveSpeed + groundVelocityX,
                rb.linearVelocity.y
            );
        }
    }
}
