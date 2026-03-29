using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject mixtureParent;
    [SerializeField] private Vector2 spawnOffset = new Vector2(1f, 2f);
    private GameObject currentBlock;

    public void CreateBlock(ItemData item0, ItemData item1 = null, ItemData item2 = null)
    {
        // 前のブロック削除
        if (currentBlock != null)
        {
            Destroy(currentBlock);
        }

        //nullじゃないものだけリストに
        List<ItemData> items = new List<ItemData>();
        if (item0 != null) items.Add(item0);
        if (item1 != null) items.Add(item1);
        if (item2 != null) items.Add(item2);
        items.Sort((a, b) => a.ID.CompareTo(b.ID));

        if (!item0.IsBlock) { Debug.LogWarning("Creating Non-Block Item"); }
        else
        {
            Debug.Log("Create " + item0 + "," + item1 + "," + item2);
        }

        //親オブジェクト生成
        Vector3 spawnPos = player.position + (Vector3)spawnOffset;
        GameObject parent = Instantiate( mixtureParent);
        parent.transform.position = spawnPos;
        currentBlock = parent;
        //子として配置
        float spacing = 1f;

        for (int i = 0; i < items.Count; i++)
        {
            GameObject child = Instantiate(items[i].prefab, parent.transform);

            // 縦並び
            float offset = (i - (items.Count - 1) * 0.5f) * spacing;
            child.transform.localPosition = new Vector3(0, offset, 0);
        }
    }
}

