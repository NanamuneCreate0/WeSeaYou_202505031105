using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PropellerAbility : MonoBehaviour
{
    private const float MAX_FALL_SPEED = -0.5f;

    private Rigidbody2D _rb;
    private AudioSource _audioSource;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = GameAssetManager.Instance.GameAssets.propellerSE;
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _rb.linearVelocity;

        // —Ž‰º’†‚¾‚¯“K—p
        if (velocity.y < MAX_FALL_SPEED)
        {
            velocity.y = MAX_FALL_SPEED;
            _rb.linearVelocity = velocity;
        }

        // —Ž‰º’†‚¾‚¯SE‚ðÄ¶
        if (velocity.y < 0f)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }

    private void OnDestroy()
    {
        if (_audioSource != null)
        {
            _audioSource.Stop();
        }
    }
}