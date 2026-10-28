using UnityEngine;

public class OperableObj : MonoBehaviour, IOperable//SeaSkillExcuterÇÃèÛë‘ÇéÛÇØÇƒêFÇïœÇ¶ÇÈ//IOperableÇÕinterfaceÇ∆ÇµÇƒÇÃë∂ç›âøílÇ»Ç¢Ç©Ç‡
{
    [SerializeField] private SeaSkillExecutor _seaSkillExecutor;
    private static readonly Color candidateColor = new Color(1f, 0.4f, 0.7f, 0.3f);
    private static readonly Color targetColor = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.3f);

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private bool _isCandidate;
    private bool _isTarget;

    private void Awake()
    {
        _seaSkillExecutor = GameObject.Find("SeaSkillExcutor").GetComponent<SeaSkillExecutor>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponentInChildren<Collider2D>();
    }

    private void OnEnable()
    {
        if (_seaSkillExecutor == null)
            return;

        _seaSkillExecutor.CandidateStateChanged += OnCandidateStateChanged;
        _seaSkillExecutor.TargetStateChanged += OnTargetStateChanged;
    }

    private void OnDisable()
    {
        if (_seaSkillExecutor == null)
            return;

        _seaSkillExecutor.CandidateStateChanged -= OnCandidateStateChanged;
        _seaSkillExecutor.TargetStateChanged -= OnTargetStateChanged;
    }

    public void OnCandidateStateChanged(Collider2D candidate, bool isCandidate)
    {
        if (candidate != _collider)
            return;

        _isCandidate = isCandidate;

        UpdateColor();
    }

    public void OnTargetStateChanged(Collider2D target, bool isTarget)
    {
        if (target != _collider)
            return;

        _isTarget = isTarget;

        UpdateColor();
    }
    /*
    public void UpdateColor(Color? color = null)
    {
        if (color != null)
        {
            _spriteRenderer.color = color.Value;
        }
        else
        {
            if (_isTarget)
            {
                _spriteRenderer.color = targetColor;
            }
            else if (_isCandidate)
            {
                _spriteRenderer.color = candidateColor;
            }
            else
            {
                _spriteRenderer.color = _originalColor;
            }
        }
    }*/
    private GameObject _operableObjView;

public void UpdateColor(Color? color = null)
{
    if (_operableObjView == null)
    {
        _operableObjView = Instantiate(
            GameAssetManager.Instance.GameAssets.operableObjViewPrefab,
            transform);
    }

    OperableObjView view = _operableObjView.GetComponent<OperableObjView>();

    if (view == null)
    {
        return;
    }

    Color displayColor;

    if (color != null)
    {
        displayColor = color.Value;
    }
    else if (_isTarget)
    {
        displayColor = targetColor;
    }
    else if (_isCandidate)
    {
        displayColor = candidateColor;
    }
    else
    {
        displayColor = Color.clear;
    }

    view.ReflectUpdateColor(displayColor);
}
     
}