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
    [SerializeField]
    Anim_ChikyuSkillMix MyAnim;
    [SerializeField]
    Sprite Transparent;

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
            MyAnim.StartAnim_MixFailuer(TableItems[0], TableItems[1]);

            TableItems.Clear();NumberOfSubmittedItem.Clear();
            SetItems();

            MyChikyuSkillHand.RefreshHandItemsBool();
            MyChikyuSkillHand.SetItem(0);

            IsMixing = false;
        }
        else if (item0 != null)
        {
            Debug.Log("成功");
            MyAnim.StartAnim_Mix(TableItems[0], TableItems[1],item0);

            //アイテム操作
            int CursorNum = NumberOfSubmittedItem[1];
            NumberOfSubmittedItem.Sort((a, b) => b.CompareTo(a));// 降順にソート
            foreach (int i in NumberOfSubmittedItem)
            {
                MyChikyuSkillHand.HandItems.RemoveAt(i);
                MyChikyuSkillHand.HandItems.Insert(i,null);
            }
            MyChikyuSkillHand.HandItems[CursorNum]=item0;//Add(item0);//
            MyChikyuSkillHand.ConfirmStaticItemList(false);
            MyItemDisplayer.SetItemDisplay(false);

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
        for (int i = 0; i < 2; ++i)
        {
            if (TableItems.Count > i)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = TableItems[i].sprite;
                Debug.Log(TableItems[i].name + " - OnTable");
            }
            else if (TableItems.Count <= i)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = Transparent;
            }
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

    
}
