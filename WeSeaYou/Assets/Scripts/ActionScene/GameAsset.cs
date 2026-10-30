using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameAssets")]
public class GameAssets : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject bombHitboxPrefab;
    public GameObject bombHitboxPrefab2;
    public GameObject operableObjViewPrefab;
    [Header("SE")]
    public AudioClip landingSE;
    public AudioClip heavyLandingSE;
    public AudioClip bombSE;
    public AudioClip getSE;
    public AudioClip MenuSE;
    public AudioClip cursorSE;
    public AudioClip decideSE;
    public AudioClip seaSkillSE;
    public AudioClip stepSE;
    public AudioClip propellerSE;
}