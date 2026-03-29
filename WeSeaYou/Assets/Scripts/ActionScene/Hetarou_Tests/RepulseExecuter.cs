using EffekseerTool;
using System.Collections.Generic;
using UnityEngine;

public class RepulseExecuter : MonoBehaviour, ISeaSkill
{
    [SerializeField] private GameObject _repulseAppearance;
    [SerializeField] private GameObject _skillCircleOut;
    [SerializeField] private GameObject _skillCircleIn;
    [SerializeField] private float _pushRadiusMax = 10f;
    [SerializeField] private float _pushRadiusMin = 1f;
    [SerializeField] private float _pushSpeed = 5f;
    [SerializeField] private LayerMask _targetLayer;

    private List<Collider2D> _hitResults = new List<Collider2D>();
    private ContactFilter2D _contactFilter;
    private bool _isPushing = false;

    void Start()
    {
        // フィルターの設定（LayerMaskをここで指定）
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(_targetLayer);
        _contactFilter.useLayerMask = true;
    }

    public void Execute()
    {
        Debug.Log("StartRepulslMode");
        _isPushing = true;
        _repulseAppearance.SetActive(true);
    }

    void FixedUpdate()
    {
        if (!_isPushing) return;

        int count = Physics2D.OverlapCircle(
            transform.position, _pushRadiusMax, _contactFilter, _hitResults
        );

        Debug.Log($"{count}");

        for (int i = 0; i < count; i++)
        {
            float dist = Vector2.Distance(transform.position, _hitResults[i].transform.position);
            if (_hitResults[i].CompareTag("UtyuSkillItem") && dist < _pushRadiusMax)
            {
                Push(_hitResults[i].transform);
            }
        }
    }

    private void Push(Transform target)
    {
        Vector2 direction = -((Vector2)transform.position - (Vector2)target.position).normalized;

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * _pushSpeed);
        }
        else
        {
            target.position += (Vector3)direction * _pushSpeed * Time.fixedDeltaTime;
        }
    }

    public void End()
    {
        Debug.Log("EndRepulslMode");
        _isPushing = false;
        _repulseAppearance.SetActive(false);
    }
}
