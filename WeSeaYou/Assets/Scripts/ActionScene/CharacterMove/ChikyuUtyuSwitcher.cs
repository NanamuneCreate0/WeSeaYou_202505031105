using UnityEngine;

public class ChikyuUtyuSwitcher : MonoBehaviour
{
    [SerializeField] GameObject objA;
    [SerializeField] GameObject objB;

    SpriteRenderer spriteA;
    SpriteRenderer spriteB;
    Animator animatorA;
    Animator animatorB;

    Sprite originalSpriteA;
    Sprite originalSpriteB;
    RuntimeAnimatorController originalAnimatorA;
    RuntimeAnimatorController originalAnimatorB;

    void Awake()
    {
        spriteA = objA.GetComponent<SpriteRenderer>();
        spriteB = objB.GetComponent<SpriteRenderer>();

        animatorA = objA.GetComponent<Animator>();
        animatorB = objB.GetComponent<Animator>();

        // ç≈èâÇÃèÛë‘Çï€ë∂
        originalSpriteA = spriteA.sprite;
        originalSpriteB = spriteB.sprite;

        originalAnimatorA = animatorA.runtimeAnimatorController;
        originalAnimatorB = animatorB.runtimeAnimatorController;
    }

    void OnEnable()
    {
        ActionModeChanger.ActionModeChangeEvent += GetActionModeChange;
    }

    void OnDisable()
    {
        ActionModeChanger.ActionModeChangeEvent -= GetActionModeChange;
    }

    void GetActionModeChange(
        ActionModeChanger.ActionModeType a,
        ActionModeChanger.ActionModeType b)
    {
        if (a == ActionModeChanger.ActionModeType.UtyuSkill)
        {
            // ç≈èâÇÃèÛë‘Ç…ñﬂÇ∑
            spriteA.sprite = originalSpriteA;
            spriteB.sprite = originalSpriteB;

            animatorA.runtimeAnimatorController = originalAnimatorA;
            animatorB.runtimeAnimatorController = originalAnimatorB;
        }
        else if (a == ActionModeChanger.ActionModeType.ChikyuSkill)
        {
            // ç≈èâÇ∆ãtÇÃèÛë‘Ç…Ç∑ÇÈ
            spriteA.sprite = originalSpriteB;
            spriteB.sprite = originalSpriteA;

            animatorA.runtimeAnimatorController = originalAnimatorB;
            animatorB.runtimeAnimatorController = originalAnimatorA;
        }
    }
}