using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeaSkillExecuter : MonoBehaviour, ISeaSkill
{
    [SerializeField] private GameObject _effectObj;
    [SerializeField] private GameObject _targettingObj;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _holdTimeLv1 = 1f;
    [SerializeField] private float _holdTimeLv2 = 2f;
    [SerializeField] private float _holdTimeLv3 = 3f;
    [SerializeField] private float _holdThreshold = 0.5f; // 長押し判定の秒数
    [SerializeField] private float _pullRadiusMax = 10f;
    [SerializeField] private float _pullRadiusMin = 1f;
    [SerializeField] private float _pullSpeed = 5f;
    [SerializeField] private LayerMask _targetLayer;

    private float _holdTime = 0f;
    private bool _hasJumped = false;

    private List<Collider2D> _hitResults = new List<Collider2D>(); 
    private List<Collider2D> _candidates = new List<Collider2D>();
    private ContactFilter2D _contactFilter;
    private Vector2 _currentSkillActionInput;
    private Vector2 _currentSelectSeaItemInput;
    private bool _isSkill = false;
    private int _selectedIndex = 0;

    private void OnSelectSeaItemRight(InputAction.CallbackContext _) => SelectRight();
    private void OnSelectSeaItemLeft(InputAction.CallbackContext _) => SelectLeft();
    void Awake()
    {
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(_targetLayer);
        _contactFilter.useLayerMask = true;
    }

    private void OnEnable()
    {
        var selectRight = InputManager.Instance.actions.SeaSkill.SelectRight;
        var selectLeft = InputManager.Instance.actions.SeaSkill.SelectLeft;

        selectRight.started += OnSelectSeaItemRight;
        selectLeft.started += OnSelectSeaItemLeft;
    }

    public void Execute()
    {
        _effectObj.SetActive(true);
        _targettingObj.SetActive(true);
        UpdateCandidates();
    }

    void Update()
    {
        if (!_isSkill) return;
        _currentSkillActionInput = InputManager.Instance.actions.Player.SeaAction.ReadValue<Vector2>();
        //HandleSelection(0);
        HandleHoldJump();
    }

    void FixedUpdate()
    {
        if (!_isSkill) return;
        Move(_candidates[_selectedIndex].transform);
    }

    private void UpdateCandidates()
    {
        _candidates.Clear();
        int count = Physics2D.OverlapCircle(
            transform.position, _pullRadiusMax, _contactFilter, _hitResults);

        for (int i = 0; i < count; i++)
        {
            if (_hitResults[i].CompareTag("UtyuSkillItem"))
                _candidates.Add(_hitResults[i]);
        }

        _candidates.Sort((a, b) =>
            a.transform.position.x.CompareTo(b.transform.position.x));

        _selectedIndex = Mathf.Clamp(_selectedIndex, 0,
            Mathf.Max(0, _candidates.Count - 1));
        SetTarget(_candidates[_selectedIndex].transform);
        _isSkill = true;
    }

    private void SetTarget(Transform target)
    {
        _targettingObj.transform.position = target.position;
        _targettingObj.transform.SetParent(target);
    }

    private void SelectRight()
    {
        if (!_isSkill) return;
        Debug.Log("右発火");
        HandleSelection(1);
    }

    private void SelectLeft()
    {
        if (!_isSkill) return;
        Debug.Log("左発火");
        HandleSelection(-1);
    }

    // 選択切り替え（Updateで呼ぶ）
    private void HandleSelection(int addIndex)
    {
        if (_candidates.Count == 0) return;

        int newIndex = _selectedIndex + addIndex;

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(_selectedIndex);

            //_selectedIndex = Mathf.Min(_selectedIndex + 1, _candidates.Count - 1);
            newIndex = _selectedIndex + 1;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(_selectedIndex);

            //_selectedIndex = Mathf.Max(_selectedIndex - 1, 0);
            newIndex = _selectedIndex - 1;
        }*/

        //else return;

        Debug.Log(newIndex);
        //動かした後、座標が変わっている可能性を考慮し、sortし直しておく
        _candidates.Sort((a, b) =>
            a.transform.position.x.CompareTo(b.transform.position.x));

        _selectedIndex = Mathf.Clamp(newIndex, 0, _candidates.Count - 1);

        SetTarget(_candidates[_selectedIndex].transform);
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
            Jump(_candidates[_selectedIndex].transform);
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
        Debug.Log("発火");
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

    private void Move(Transform target)
    {
        Vector2 input = InputManager.Instance.actions.Player.SeaAction.ReadValue<Vector2>();

        Vector3 moveDirection = new Vector3(input.x, 0, 0);

        target.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void End()
    {
        _effectObj.SetActive(false);
        _targettingObj.SetActive(false);
        _isSkill = false;
    }
}
