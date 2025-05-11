using UnityEngine;
using UnityEngine.UI;

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

        /*foreach (Transform tf in transform)
        {
            GameObject.Destroy(tf.gameObject);
        }*/
        foreach (Item item in PublicStaticStatus.ItemList)
        {
            Debug.Log(item);
            Instantiate(MyItemDisplayerCell, Vector3.zero, Quaternion.identity, transform);
            MyItemDisplayerCell.GetComponent<Image>().sprite = item.sprite;
        }
    }
}
