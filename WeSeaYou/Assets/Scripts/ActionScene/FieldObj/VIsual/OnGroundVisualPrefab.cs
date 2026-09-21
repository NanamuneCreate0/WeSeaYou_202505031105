using UnityEngine;

public class OnGroundVisualPrefab : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float emissionDuration;

    void Start()
    {
        Invoke(nameof(StopEmission), emissionDuration);

        Destroy(gameObject, 1f);
    }

    void StopEmission()
    {
        _particleSystem.Stop(
            true,
            ParticleSystemStopBehavior.StopEmitting
        );
    }
}