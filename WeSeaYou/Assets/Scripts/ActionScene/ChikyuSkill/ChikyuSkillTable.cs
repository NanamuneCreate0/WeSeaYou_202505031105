using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChikyuSkillTable : MonoBehaviour
{
    public List<Item> TableItems = new List<Item>();
    public bool IsMixing=false;

    [SerializeField]
    GameObject ChikyuSkillTableCell;
    [SerializeField]
    GameModeController MyGameModeController;

    public void ActivationStart()
    {
        TableItems.Clear();
        SetItems();
    }

    //アイテム取得時
    public void GetItem(Item item)
    {
        TableItems.Add(item);
        SetItems();
        if (TableItems.Count == 2)
        {
            ExcuteMixItem();
        }
    }
    //アイテム2個なら合成
    void ExcuteMixItem()
    {
        IsMixing = true;
        MyGameModeController.SubGameMode = "ChikyuSkillMixing";
        StartCoroutine(nameof(MixItems));
    }


    void Update()
    {
        //選択とりやめ
        if (!IsMixing)
        {
            if (Input.GetKeyDown(KeyCode.Space) && TableItems.Count != 0)
            {
                Debug.Log(TableItems[TableItems.Count - 1].name);
                TableItems.RemoveAt(TableItems.Count - 1);
                SetItems();
            }
        }
    }


    //SetItem()
    void SetItems()
    {
        if (TableItems.Count != transform.childCount)
        {
            for (int i = 0; i < 30; i++)
            {
                if (TableItems.Count > transform.childCount)
                {
                    Instantiate(ChikyuSkillTableCell, transform);
                }
                if (TableItems.Count < transform.childCount)
                {
                    if(transform.childCount-TableItems.Count>i)//i=0が一番満たしやすい。そこから差の数だけ減らす//まぁ差は1以上にはならないんですけどね
                    {
                        Destroy(transform.GetChild(transform.childCount -1- i).gameObject);//Destroyの処理はUpdateの最後になるので注意してる
                    }
                }
            }
        }
        for (int i = 0; i < TableItems.Count; ++i)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = TableItems[i].sprite;
            Debug.Log(TableItems[i].name+" - OnTable");
        }
    }

    private IEnumerator MixItems()
    {
        //この時点で結果は確定かな

        if (FigureOutMixture(TableItems[0], TableItems[1]) == null)
        {
            Debug.Log("失敗");
            // 1秒待つ  
            yield return new WaitForSeconds(2.0f);
            //失敗
            TableItems.Clear();
            SetItems();
            IsMixing = false;
            MyGameModeController.SubGameMode = "";
        }
        else
        {
            Debug.Log("成功");
            // 1秒待つ  
            yield return new WaitForSeconds(2.0f);
            //失敗
            TableItems.Clear();
            SetItems();
            IsMixing = false;
            MyGameModeController.SubGameMode = "";
        }
    }

    Item FigureOutMixture(Item item0,Item item1)
    {
        Debug.Log("FigureOutMixture(" + item0 + "," + item1+")");
        int[] list=new int[2];

        if (item0.ID < item1.ID) { list = new int[2] { item0.ID, item1.ID }; }
        else if (item0.ID > item1.ID) { list = new int[2] { item1.ID, item0.ID }; }

        foreach (ChikyuSkillMixtureIndex mixtureIndex in ChikyuSkillCursor.MixtureDictionary)
        {
            if (mixtureIndex.RequiredMaterialsArray[0] == list[0]
                &&mixtureIndex.RequiredMaterialsArray[1] == list[1])
            {
                Debug.Log("success");
                return(mixtureIndex.MixtureItem);
            }
        }

        return (null);
    }
}
