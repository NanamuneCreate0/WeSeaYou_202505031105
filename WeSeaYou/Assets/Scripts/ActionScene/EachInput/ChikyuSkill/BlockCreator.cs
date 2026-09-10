using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private ChikyuWalk chikyuWalk;
    [SerializeField] private GameObject mixturePrefab;
    [SerializeField] private Vector2 spawnOffset = new Vector2(2f, 0f);
    private GameObject currentBlock;

    public void CreateBlock(ChikyuSkillItemData item0, ChikyuSkillItemData item1 = null, ChikyuSkillItemData item2 = null)
    {
        // 前のブロック削除
        if (currentBlock != null)
        {
            Destroy(currentBlock);
        }

        //nullじゃないものだけリストに
        List<ChikyuSkillItemData> items = new List<ChikyuSkillItemData>();
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
        Vector2 spawnPos =(Vector2) player.position + spawnOffset* (int)chikyuWalk.CurrentDirection;
        mixture.transform.position = spawnPos;

        //Ability
        /*for (int i = 0; i < items.Count; i++)
        {
            Type type = items[i].blockAbility.GetClass();
            mixture.AddComponent(type);
        }*/

        // Ability
        for (int i = 0; i < items.Count; i++)
        {
            Type type = Type.GetType(items[i].blockAbility);

            if (type != null)
            {
                mixture.AddComponent(type);
            }
            else
            {
                Debug.LogWarning("NoComponent");
            }
        }
    }
}

