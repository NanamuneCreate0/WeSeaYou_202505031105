using UnityEngine;

public class OperableObj : MonoBehaviour, IOperable//SeaSkillExcuter‚Ìó‘Ô‚ğó‚¯‚ÄF‚ğ•Ï‚¦‚é//—­‚ß‚ÌF‚ÍSeaSkillExcuter‚Ì‚È‚©‚Å•Ï‚¦‚Ä‚éB‚â‚â‚±‚µ‚¢ê‡‚ÍC³‚µ‚Ä‚à
{
    [SerializeField] private SeaSkillExecutor _seaSkillExecutor;

    [SerializeField] private Color candidateColor = new Color(1f, 0.4f, 0.7f);
    [SerializeField] private Color targetColor = Color.yellow;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private Color _originalColor;

    private bool _isCandidate;
    private bool _isTarget;

    private void Awake()
    {
        _seaSkillExecutor = GameObject.Find("SeaSkillExcutor").GetComponent<SeaSkillExecutor>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponentInChildren<Collider2D>();

        _originalColor = _spriteRenderer.color;
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

    private void UpdateColor()
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
}