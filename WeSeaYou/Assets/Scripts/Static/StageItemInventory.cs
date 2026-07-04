using System;
using System.Collections.Generic;

public static class StageItemInventory
{
    private static readonly List<StageItemData> _items = new();

    public static IReadOnlyList<StageItemData> Items => _items;

    public static event Action OnChanged;

    public static void Add(StageItemData item)
    {
        _items.Add(item);
        OnChanged?.Invoke();
    }

    public static bool Remove(StageItemData item)
    {
        bool removed = _items.Remove(item);

        if (removed)
        {
            OnChanged?.Invoke();
        }

        return removed;
    }

    public static void Clear()
    {
        _items.Clear();
        OnChanged?.Invoke();
    }

    public static bool Contains(StageItemData item)
    {
        return _items.Contains(item);
    }
}