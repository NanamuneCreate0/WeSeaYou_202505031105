using System.Collections.Generic;
using UnityEngine;

public class UtyuSkillOutput : MonoBehaviour
{
    [SerializeField]
    ActionModeChanger MyActionModeChanger;
    [SerializeField]
    GameObject UtyuSkillItem;
    [SerializeField]
    GameObject MyUtyuSkillUI;
    [SerializeField]
    GameObject MyUtyu;
    const float moveSpeed = 5;
    Item TableItem;
    int NumberOfSubmittedItem;
    GameObject HandlingObj;

    public void ChatchSubmitItem(Item item, int num)
    {
        TableItem=item;
        NumberOfSubmittedItem=num;
        if(MyActionModeChanger.ActionMode==11)
        {
            MyActionModeChanger.ActionMode=21;
            MyUtyuSkillUI.SetActive(false);
        }
        Debug.Log("ここで" + TableItem.name + "動かす");
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
        /*
        //入力取得（-1〜1の範囲）
        float horizontal = Input.GetAxis("Horizontal");// 左右(A,D or ←,→)
        float vertical = Input.GetAxis("Vertical");//上下(W,S or ↑,↓)
        //移動方向ベクトル
        Vector2 move = new Vector2(horizontal, vertical);

        //移動（時間に依存しないようにdeltaTimeを掛ける）
        HandlingObj.transform.Translate(move * moveSpeed * Time.deltaTime);*/
        HandlingObj.transform.position=new Vector2 (MyUtyu.transform.position.x, HandlingObj.transform.position.y+ Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime);

        //UtyuSkill実行終わらせる
        if(Input.GetKeyDown(KeyCode.X)|| Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(HandlingObj);
            MyActionModeChanger.ActionMode = 11;
            MyUtyuSkillUI.SetActive(true);
        }
    }
}
