using System.Collections.Generic;
using UnityEngine;

public class UtyuSkillTable : MonoBehaviour
{
    Item TableItem;
    int NumberOfSubmittedItem;

    public void ChatchSubmitItem(Item item, int num)
    {
        TableItem=item;
        NumberOfSubmittedItem=num;
        Excute();
    }

    void Excute()
    {
        Debug.Log("‚±‚±‚Å" + TableItem.name+"“®‚©‚·");
    }
}
