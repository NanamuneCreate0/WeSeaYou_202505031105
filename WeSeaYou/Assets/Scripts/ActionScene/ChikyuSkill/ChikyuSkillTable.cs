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
    GameModeController MyGameModeController;
    [SerializeField]
    ChikyuSkillHand MyChikyuSkillHand;

    List<int> NumberOfSubmittedItem = new List<int>();

    public void ActivationStart()
    {
        TableItems.Clear();
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
        MyGameModeController.SubGameMode = "ChikyuSkillMixing";
        StartCoroutine(nameof(MixItems));
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
        TableItems.RemoveAt(TableItems.Count - 1);
        NumberOfSubmittedItem.RemoveAt(NumberOfSubmittedItem.Count - 1);
        SetItems();
        MyChikyuSkillHand.ChatchUndoSubmitItem();
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

    private IEnumerator MixItems()
    {
        Item item0 = FigureOutMixture(TableItems[0], TableItems[1]);

        if (item0 == null)
        {
            Debug.Log("失敗");
            yield return new WaitForSeconds(2.0f);// 数秒待つ
            MyChikyuSkillHand.CommitHandItemValue();
            MyChikyuSkillHand.SetItem(0);
            TableItems.Clear();
            SetItems();
            IsMixing = false;
            MyGameModeController.SubGameMode = "";
        }
        else
        {
            Debug.Log("成功");
            yield return new WaitForSeconds(2.0f);// 数秒待つ

            //アイテム操作
            NumberOfSubmittedItem.Sort((a, b) => b.CompareTo(a));// 降順にソート
            foreach (int i in NumberOfSubmittedItem)
            {
                PublicStaticStatus.ItemList.RemoveAt(i);
            }

            MyChikyuSkillHand.CommitHandItemValue();
            MyChikyuSkillHand.SetItem(0);
            TableItems.Clear();
            SetItems();
            IsMixing = false;
            MyGameModeController.SubGameMode = "";
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
                Debug.Log("successMix");
                return (mixtureIndex.MixtureItem);
            }
        }
        return (null);
    }
}
