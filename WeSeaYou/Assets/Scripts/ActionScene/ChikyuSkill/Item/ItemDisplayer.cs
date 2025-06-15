using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemDisplayer : MonoBehaviour
{
    [SerializeField]
    GameObject MyItemDisplayCell;
    void Start()
    {
        
    }

    void Update()
    {
    }

    public void SetItemDisplay(bool ExcuteSort)
    {
        //StartCoroutine("ExcuteSetItemDisplay");
        //ÉZÉãÇÃêîÇçáÇÌÇπÇÈ
        if (PublicStaticStatus.ItemList.Count != transform.childCount)
        {
            for (int i = 0; i < 30; i++)
            {
                if (PublicStaticStatus.ItemList.Count > transform.childCount)
                {
                    Instantiate(MyItemDisplayCell, transform);
                }
                if (PublicStaticStatus.ItemList.Count < transform.childCount)
                {
                    if (transform.childCount - PublicStaticStatus.ItemList.Count > i)//i=0Ç™àÍî‘ñûÇΩÇµÇ‚Ç∑Ç¢ÅBÇªÇ±Ç©ÇÁç∑ÇÃêîÇæÇØå∏ÇÁÇ∑//Ç‹Çüç∑ÇÕ1à»è„Ç…ÇÕÇ»ÇÁÇ»Ç¢ÇÒÇ≈Ç∑ÇØÇ«ÇÀ
                    {
                        Destroy(transform.GetChild(transform.childCount - 1 - i).gameObject);//DestroyÇÃèàóùÇÕUpdateÇÃç≈å„Ç…Ç»ÇÈÇÃÇ≈íçà”ÇµÇƒÇÈ
                    }
                }
            }

        }
        if (ExcuteSort) { PublicStaticStatus.ItemList.Sort((a, b) => a.ID.CompareTo(b.ID)); }
        for (int i = 0; i < PublicStaticStatus.ItemList.Count; ++i)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = PublicStaticStatus.ItemList[i].sprite;
        }
    }
}
