using System.Collections.Generic;

public class TextTable
{
    private readonly Dictionary<string, TextEntry> _table = new();

    public TextTable(TextDataSO source)
    {
        foreach (var e in source.Entries)
            _table[e.ID] = e;
    }

    public bool TryGet(string id, out TextEntry entry)
        => _table.TryGetValue(id, out entry);
}