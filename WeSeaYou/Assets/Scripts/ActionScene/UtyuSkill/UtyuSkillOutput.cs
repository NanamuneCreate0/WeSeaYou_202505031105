using System;
using UnityEngine;

public class UtyuSkillOutput : MonoBehaviour
{
    public Item TableItem;
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
    public void ChatchSubmitItem(Item item, int num)
    {
        TableItem=item;
        NumberOfSubmittedItem=num;
        if(MyActionModeChanger.ActionMode==11)
        {
            MyActionModeChanger.ChangeActionMode(21, 11);
            MyUtyuSkillUI.SetActive(false);
        }
        GameObject go = Instantiate(UtyuSkillItem, transform.position, Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sprite = TableItem.sprite;
        HandlingObj = go;
    }

    void Update()
    {
        if (MyActionModeChanger.ActionMode == 21) { Update_Outputing(); }
    }
    
    void Update_Outputing()
    {
        HandlingObj.transform.position=new Vector2 (MyUtyu.transform.position.x, HandlingObj.transform.position.y+ Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime);

    }
    void GetActionModeChange(int a,int b)
    {
        if(a == 11 && b == 21)
        {
            Destroy(HandlingObj);
            MyUtyuSkillUI.SetActive(true);
        }
    }
}
