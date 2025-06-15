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

    //�A�C�e���擾��
    public void ChatchSubmitItem(Item item, int num)
    {
        TableItems.Add(item);
        NumberOfSubmittedItem.Add(num);
        SetItems();
        if (TableItems.Count == 2)//�A�C�e��2�Ȃ獇��
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
                    if (transform.childCount - TableItems.Count > i)//i=0����Ԗ������₷���B�������獷�̐��������炷//�܂�����1�ȏ�ɂ͂Ȃ�Ȃ���ł����ǂ�
                    {
                        Destroy(transform.GetChild(transform.childCount - 1 - i).gameObject);//Destroy�̏�����Update�̍Ō�ɂȂ�̂Œ��ӂ��Ă�
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
            Debug.Log("���s");
            yield return new WaitForSeconds(2.0f);// ���b�҂�
            MyChikyuSkillHand.CommitHandItemValue();
            MyChikyuSkillHand.SetItem(0);
            TableItems.Clear();
            SetItems();
            IsMixing = false;
            MyGameModeController.SubGameMode = "";
        }
        else
        {
            Debug.Log("����");
            yield return new WaitForSeconds(2.0f);// ���b�҂�

            //�A�C�e������
            NumberOfSubmittedItem.Sort((a, b) => b.CompareTo(a));// �~���Ƀ\�[�g
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
