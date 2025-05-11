using UnityEngine;

public class FieldItem : MonoBehaviour
{
    [SerializeField]
    Item MyItem;

    bool isTouching = false;

    void Update()
    {
        if (isTouching&&Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("GetItem");
            PublicStaticStatus.ItemList.Add(MyItem);
            GameObject.Find("ItemDisplayer").GetComponent<ItemDisplayer>().SetItemDisplay();
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("player")) { isTouching = true; }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player")) { isTouching = false; }
    }

}
