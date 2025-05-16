using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemDisplayer : MonoBehaviour
{
    [SerializeField]
    GameObject MyItemDisplayerCell;
    void Start()
    {
        
    }

    void Update()
    {
    }

    public void SetItemDisplay()
    {
        Debug.Log("set");
        StartCoroutine("ChangeColor");

        /*
        //ÉZÉãÇÃêîÇçáÇÌÇπÇÈ
        if(PublicStaticStatus.ItemList.Count !=transform.childCount)
        {
            Debug.Log(PublicStaticStatus.ItemList.Count);
            for (int i = 0; i < 30; i++)
            {
                if (PublicStaticStatus.ItemList.Count < transform.childCount)
                {
                    Instantiate(MyItemDisplayerCell, transform);
                }
                if (PublicStaticStatus.ItemList.Count > transform.childCount)
                {
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
            
        }

        for (int i = 0; i < PublicStaticStatus.ItemList.Count; ++i)
        {
            Debug.Log(PublicStaticStatus.ItemList[i]);
            transform.GetChild(i).GetComponent<Image>().sprite = PublicStaticStatus.ItemList[i].sprite;
        }*/
    }

    IEnumerator ChangeColor()
    {

        //ÉZÉãÇÃêîÇçáÇÌÇπÇÈ
        if (PublicStaticStatus.ItemList.Count != transform.childCount)
        {
            Debug.Log(PublicStaticStatus.ItemList.Count);
            Debug.Log(transform.childCount);
            for (int i = 0; i < 30; i++)
            {
                if (PublicStaticStatus.ItemList.Count > transform.childCount)
                {
                    Instantiate(MyItemDisplayerCell, transform);
                }
                if (PublicStaticStatus.ItemList.Count < transform.childCount)
                {
                    Destroy(transform.GetChild(0).gameObject);
                }
            }

        }
        yield return null;
        for (int i = 0; i < PublicStaticStatus.ItemList.Count; ++i)
        {
            Debug.Log(PublicStaticStatus.ItemList[i]);
            transform.GetChild(i).GetComponent<Image>().sprite = PublicStaticStatus.ItemList[i].sprite;
        }
    }
}
