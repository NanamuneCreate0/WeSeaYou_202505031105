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

    public void SetItemDisplay()
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
                    Destroy(transform.GetChild(0).gameObject);
                }
            }

        }
        for (int i = 0; i < PublicStaticStatus.ItemList.Count; ++i)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = PublicStaticStatus.ItemList[i].sprite;
        }
    }
}
