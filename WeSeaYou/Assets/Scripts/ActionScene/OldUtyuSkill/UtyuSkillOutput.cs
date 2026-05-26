using System;
using UnityEngine;
using static ActionModeChanger;

public class UtyuSkillOutput : MonoBehaviour
{
    public ItemData TableItem;
    [SerializeField]
    ActionModeChanger MyActionModeChanger;
    [SerializeField]
    GameObject UtyuSkillItem;
    [SerializeField]
    GameObject MyUtyuSkillUI;
    [SerializeField]
    UtyuSkillHand MyUtyuSkillHand;
    [SerializeField]
    GameObject MyUtyu;
    const float moveSpeed = 5;
    int NumberOfSubmittedItem;
    GameObject HandlingObj;
    private void OnEnable()
    {
        ActionModeChanger.ActionModeChangeEvent += GetActionModeChange;
    }
    private void OnDisable()
    {
        ActionModeChanger.ActionModeChangeEvent -= GetActionModeChange;
    }
    public void ChatchSubmitItem(ItemData item, int num)
    {
        TableItem=item;
        NumberOfSubmittedItem=num;
        if(MyActionModeChanger.ActionMode== ActionModeType.UtyuSkill)
        {
            MyActionModeChanger.ChangeActionMode(ActionModeType.UtyuSkillActive, ActionModeType.UtyuSkill);
            MyUtyuSkillUI.SetActive(false);
        }
        GameObject go = Instantiate(UtyuSkillItem, transform.position, Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sprite = TableItem.sprite;
        HandlingObj = go;
    }

    public void LoseItem()
    {
        Debug.Log(NumberOfSubmittedItem);
        MyUtyuSkillHand.HandItems.RemoveAt(NumberOfSubmittedItem);
        MyUtyuSkillHand.ConfirmStaticItemList(false);
        //MyUtyuSkillHand.SetItem(0);//どうせ消える
    }

    void Update()
    {
        if (MyActionModeChanger.ActionMode == ActionModeType.UtyuSkillActive) { Update_Outputing(); }
    }
    
    void Update_Outputing()
    {
        HandlingObj.transform.position=new Vector2 (MyUtyu.transform.position.x, HandlingObj.transform.position.y+ Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime);

    }
    void GetActionModeChange(ActionModeType a, ActionModeType b)
    {
        if(a == ActionModeType.UtyuSkill && b==ActionModeType.UtyuSkillActive)
        {
            Destroy(HandlingObj);
            MyUtyuSkillUI.SetActive(true);
        }
    }
}
