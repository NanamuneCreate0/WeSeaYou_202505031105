using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private ChikyuWalk chikyuWalk;
    [SerializeField] private GameObject mixturePrefab;
    [SerializeField] private Vector2 spawnOffset = new Vector2(2f, 0f);
    private GameObject currentBlock;
    private BlockAbilityExcuter blockAbilityExcuter;

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

        //生成
        GameObject mixture = Instantiate( mixturePrefab);
        currentBlock = mixture;
        //位置
        Vector2 spawnPos =(Vector2) player.position + spawnOffset* (int)chikyuWalk.LastDirection;
        mixture.transform.position = spawnPos;

        //Ability
        blockAbilityExcuter = mixture.GetComponent<BlockAbilityExcuter>();
        for (int i = 0; i < items.Count; i++)
        {
            blockAbilityExcuter.BlockAbilities.Add(items[i].blockAbility);
        }
    }
}

