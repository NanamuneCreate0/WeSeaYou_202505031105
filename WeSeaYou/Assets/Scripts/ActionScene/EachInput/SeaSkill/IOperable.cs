using UnityEngine;

public interface IOperable
{
    void OnCandidateStateChanged(Collider2D candidate, bool isCandidate);
    void OnTargetStateChanged(Collider2D target, bool isTarget);
}