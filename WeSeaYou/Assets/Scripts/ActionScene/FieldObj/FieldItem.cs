using UnityEngine;

public class FieldItem : MonoBehaviour
{
    [SerializeField]
    ChikyuSkillItemData MyItem;

    GameModeController MyGameModeController;

    bool isTouching = false;
    void Start()
    {
        MyGameModeController = GameObject.Find("GameModeController").GetComponent<GameModeController>();
    }

    void Update()
    {
        if (isTouching&&Input.GetKeyDown(KeyCode.C)&&MyGameModeController.GameMode=="Action")
        {
            Debug.Log("GetItem");
            PublicStaticStatus.ChikyuSkillItemList.Add(MyItem);
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
