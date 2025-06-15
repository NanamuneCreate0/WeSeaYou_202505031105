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
    public bool IsMixing = false;

    [SerializeField]
    GameObject ChikyuSkillTableCell;
    [SerializeField]
    ChikyuSkillHand MyChikyuSkillHand;
    [SerializeField]
    ItemDisplayer MyItemDisplayer;

    List<int> NumberOfSubmittedItem = new List<int>();

    public void ActivationStart()
    {
        TableItems.Clear();
        NumberOfSubmittedItem.Clear();
        SetItems();
    }

    //アイテム取得時
    public void ChatchSubmitItem(Item item, int num)
    {
        TableItems.Add(item);
        NumberOfSubmittedItem.Add(num);
        SetItems();
        if (TableItems.Count == 2)//アイテム2個なら合成
        {
            ExcuteMixItem();
        }
    }
    void ExcuteMixItem()
    {
        IsMixing = true;
        
        Item item0 = FigureOutMixture(TableItems[0], TableItems[1]);

        if (item0 == null)
        {
            Debug.Log("失敗");
            //MyChikyuSkillHand.CommitHandItemValue();

            TableItems.Clear();NumberOfSubmittedItem.Clear();
            SetItems();

            MyChikyuSkillHand.RefreshHandItemsBool();
            MyChikyuSkillHand.SetItem(0);

            IsMixing = false;
        }
        else if (item0 != null)
        {
            Debug.Log("成功");
            StartAnim_Mix(TableItems[0], TableItems[1]);
            int CursorNum = NumberOfSubmittedItem[1];
            //アイテム操作
            NumberOfSubmittedItem.Sort((a, b) => b.CompareTo(a));// 降順にソート
            foreach (int i in NumberOfSubmittedItem)
            {
                MyChikyuSkillHand.HandItems.RemoveAt(i);
                MyChikyuSkillHand.HandItems.Insert(i,null);
            }
            MyChikyuSkillHand.HandItems[CursorNum]=item0;//Add(item0);//
            MyChikyuSkillHand.ConfirmStaticItemList(false);
            MyItemDisplayer.SetItemDisplay(false);

            //MyChikyuSkillHand.CommitHandItemValue();
            TableItems.Clear();NumberOfSubmittedItem.Clear();
            SetItems();

            MyChikyuSkillHand.RefreshHandItemsBool();
            MyChikyuSkillHand.SetItem(0);


            IsMixing = false;
        }
    }


    void Update()
    {
        if (!IsMixing && Input.GetKeyDown(KeyCode.Space) && TableItems.Count != 0)
        {
            UnSubmitItem();
        }
    }

    void UnSubmitItem()
    {
        TableItems.RemoveAt(TableItems.Count - 1);NumberOfSubmittedItem.RemoveAt(NumberOfSubmittedItem.Count - 1);
        SetItems();
        MyChikyuSkillHand.RefreshHandItemsBool();
        MyChikyuSkillHand.SetItem(0);
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
                    if (transform.childCount - TableItems.Count > i)//i=0が一番満たしやすい。そこから差の数だけ減らす//まぁ差は1以上にはならないんですけどね
                    {
                        Destroy(transform.GetChild(transform.childCount - 1 - i).gameObject);//Destroyの処理はUpdateの最後になるので注意してる
                    }
                }
            }
        }
        for (int i = 0; i < TableItems.Count; ++i)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = TableItems[i].sprite;
            Debug.Log(TableItems[i].name + " - OnTable");
        }
    }


    Item FigureOutMixture(Item item0, Item item1)
    {
        Debug.Log("FigureOutMixture(" + item0 + "," + item1 + ")");
        Item[] list = new Item[2];

        if (item0.ID < item1.ID) { list = new Item[2] { item0, item1 }; }
        else if (item0.ID > item1.ID) { list = new Item[2] { item1, item0 }; }

        foreach (ChikyuSkillMixtureIndex mixtureIndex in ChikyuSkillCursor.MixtureDictionary)
        {
            if (mixtureIndex.RequiredMaterialsArray[0] == list[0]
                && mixtureIndex.RequiredMaterialsArray[1] == list[1])
            {
                return (mixtureIndex.MixtureItem);
            }
        }
        return (null);
    }

    void StartAnim_Mix(Item item0, Item item1)
    {
        /*GameObject go=GameObject.Find("Anim_Mix");
        go.transform.GetChild(0).GetComponent<Image>().sprite= item0.sprite;
        go.transform.GetChild(1).GetComponent<Image>().sprite= item1.sprite;*/
    }
}
