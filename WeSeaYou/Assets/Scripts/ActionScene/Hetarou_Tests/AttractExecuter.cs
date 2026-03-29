using System.Collections.Generic;
using UnityEngine;

public class AttractExecuter : MonoBehaviour, ISeaSkill
{
    [SerializeField] private GameObject _attractAppearance;
    [SerializeField] private GameObject _skillCircleOut;
    [SerializeField] private GameObject _skillCircleIn;
    [SerializeField] private float _pullRadiusMax = 10f;
    [SerializeField] private float _pullRadiusMin = 1f;
    [SerializeField] private float _pullSpeed = 5f;
    [SerializeField] private LayerMask _targetLayer;

    private List<Collider2D> _hitResults = new List<Collider2D>();
    private ContactFilter2D _contactFilter;
    private bool _isPulling = false;

    void Start()
    {
        // フィルターの設定（LayerMaskをここで指定）
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(_targetLayer);
        _contactFilter.useLayerMask = true;
    }
    public void Execute()
    {
        Debug.Log("StartAttractMode");
        _isPulling = true;
        _attractAppearance.SetActive(true);
    }

    void FixedUpdate()
    {
        if (!_isPulling) return;

        int count = Physics2D.OverlapCircle(
            transform.position, _pullRadiusMax, _contactFilter, _hitResults
        );

        Debug.Log($"{count}");

        for (int i = 0; i < count; i++)
        {
            float dist = Vector2.Distance(transform.position, _hitResults[i].transform.position);
            if (_hitResults[i].CompareTag("UtyuSkillItem") && dist > _pullRadiusMin)
            {
                Pull(_hitResults[i].transform);
            }
        }
    }

    void Pull(Transform target)
    {
        Vector2 direction = ((Vector2)transform.position - (Vector2)target.position).normalized;

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * _pullSpeed);
        }
        else
        {
            target.position += (Vector3)direction * _pullSpeed * Time.fixedDeltaTime;
        }
    }

    public void End()
    {
        Debug.Log("EndAttractMode");
        _isPulling = false;
        _attractAppearance.SetActive(false);
    }
}