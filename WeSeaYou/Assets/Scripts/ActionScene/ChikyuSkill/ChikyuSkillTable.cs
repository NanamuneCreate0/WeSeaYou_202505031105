using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChikyuSkillTable : MonoBehaviour
{
    //DictionaryÇ…ë•ÇËçXêV
    //
    [SerializeField]
    GameObject ChikyuSkillTableCell;
    [SerializeField]
    ChikyuSkillCursor MyChikyuSkillCursor;

    public
        List<Item> TableItems = new List<Item>();

    public void GetItem(Item item)
    {
        TableItems.Add(item);
        SetItems();
        if (TableItems.Count == 2) { MyChikyuSkillCursor.ChangeCursorMode(1); }
        else if (TableItems.Count < 2) { MyChikyuSkillCursor.ChangeCursorMode(0); }
    }

    void Update()
    {
        //ëIëÇ∆ÇËÇ‚Çﬂ
        if (Input.GetKeyDown(KeyCode.Space)&& TableItems.Count != 0)
        {
            Debug.Log(TableItems[TableItems.Count - 1].name);
            TableItems.RemoveAt(TableItems.Count-1);
            if (TableItems.Count == 2) { MyChikyuSkillCursor.ChangeCursorMode(1); }
            else if (TableItems.Count < 2) { MyChikyuSkillCursor.ChangeCursorMode(0); }
            SetItems();
        }
    }

    void SetItems()
    {
        //StartCoroutine("ExcuteSetItem");
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
                    Destroy(transform.GetChild(0).gameObject);
                    Debug.Log("destr"+ transform.childCount);
                }
            }
        }
        for (int i = 0; i < TableItems.Count; ++i)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = TableItems[i].sprite;
            Debug.Log(TableItems[i].name+"fafda");
        }
        Debug.Log(transform.childCount);

    }
}
