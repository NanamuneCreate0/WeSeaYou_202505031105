using System;
using System.Collections.Generic;

public static class PublicStaticItem
{
    public static List<ChikyuSkillItemData> ChikyuSkillItemList = new List<ChikyuSkillItemData>();
    public static List<CollectionItemData> CollectionItemList = new List<CollectionItemData>();

    public static List<StageItemData> StageItemList = new List<StageItemData>();//SceneFinalizer‚É‚Ä”jŠü

    public static event Action OnStageItemChanged;
    public static event Action OnCollectionItemChanged;

    public static void AddStageItem(StageItemData item)
    {
        StageItemList.Add(item);
        OnStageItemChanged?.Invoke();
    }

    public static bool RemoveStageItem(StageItemData item)
    {
        bool removed = StageItemList.Remove(item);

        if (removed)
        {
            OnStageItemChanged?.Invoke();
        }

        return removed;
    }

    public static void ClearStageItem()
    {
        StageItemList.Clear();
        OnStageItemChanged?.Invoke();
    }

    public static bool ContainsStageItem(StageItemData item)
    {
        return StageItemList.Contains(item);
    }

    public static void AddCollectionItem(CollectionItemData item)
    {
        if (!CollectionItemList.Contains(item)) { CollectionItemList.Add(item); }
        OnCollectionItemChanged?.Invoke();
    }
}