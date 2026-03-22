using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChikyuSkillTable : MonoBehaviour
{
    public List<ItemData> TableItems = new List<ItemData>();

    [SerializeField]
    private BlockCreator blockCreater;
    [SerializeField]
    private ActionModeChanger actionModeChanger;
    [SerializeField]
    private Sprite Transparent;

    public void ActivationStart()
    {
        TableItems.Clear();
        SetItems();
    }

    public void CatchSubmitItem(ItemData item)
    {
        Debug.Log("Catch "+item.name);
        TableItems.Add(item);
        SetItems();
        if (TableItems.Count == 3)//ƒAƒCƒeƒ€3ŒÂ‚È‚ç‡¬
        {
            CatchSubmitDone();
        }
    }
    
    public void CatchSubmitDone()
    {
        actionModeChanger.ChangeActionMode(ActionModeChanger.ActionModeType.ChikyuView, ActionModeChanger.ActionModeType.ChikyuSkill);

        ItemData a = TableItems[0];
        ItemData b = TableItems.Count >= 2 ? TableItems[1] : null;
        ItemData c = TableItems.Count >= 3 ? TableItems[2] : null;
        blockCreater.CreateBlock(a, b, c);
    }

    void SetItems()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (TableItems.Count > i)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = TableItems[i].sprite;
            }
            else if (TableItems.Count <= i)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = Transparent;
            }
        }
    }

    
}
