using UnityEngine;

public class UtyuSkillItemEventListener : MonoBehaviour
{
    [SerializeField] Item requiredItem;
    [SerializeField]
    ActionModeChanger MyActionModeChanger;
    [SerializeField]
    GameObject MyUtyuSkillUI;
    [SerializeField]
    UtyuSkillHand MyUtyuSkillHand;
    [SerializeField]
    UtyuSkillOutput MyUtyuSkillOutput;
    [SerializeField]
    MonoBehaviour component;

    const float requiredTime = 3f;
    const string targetTag = "UtyuSkillItem";
    GameObject MyUtyuSkillItem;
    private float stayTime = 0f;
    private bool isTouching = false;

    private void Update()
    {
        if (isTouching)
        {
            stayTime += Time.deltaTime;

            if (stayTime >= requiredTime)
            {
                if (MyActionModeChanger.ActionMode != 21)
                {
                    Debug.LogError("ActionModeWrong");
                }

                Activate();
                isTouching = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            isTouching = true;
            MyUtyuSkillItem = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            stayTime = 0f;
            isTouching = false;
        }
    }

    private void Activate()
    {
        if(MyUtyuSkillOutput.TableItem==requiredItem)
        {
            Debug.Log("ãNìÆê¨å˜");
            MyUtyuSkillOutput.LoseItem();
            MyActionModeChanger.ChangeActionMode(1, 21);
            MyUtyuSkillHand.OnDisableAndReset();
            MyUtyuSkillUI.SetActive(false);
            Destroy(MyUtyuSkillItem);

            ExcuteEvent();
        }
        else
        {
            Debug.Log("ãNìÆé∏îs");
            MyActionModeChanger.ChangeActionMode(11, 21);
        }
    }
    void ExcuteEvent()
    {
        if(component is UtyuSkillEventInterface ef)
        {
            ef.OnActivate();
        }
    }
}
