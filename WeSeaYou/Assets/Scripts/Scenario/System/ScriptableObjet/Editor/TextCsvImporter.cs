// Assets/Editor/TextCsvImporter.cs
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public static class TextCsvImporter
{
    private const string CsvFolder = "Assets/Scripts/Scenario/CSV";
    private const string OutputFolder = "Assets/Scripts/Scenario/Data";

    // ── 入口1: 右クリックで選択したものだけ変換(複数選択対応) ──
    [MenuItem("Assets/シナリオ/選択したCSVをSOに変換")]
    public static void ImportSelected()
    {
        foreach (var obj in Selection.objects)   // activeObjectでなくobjects=複数選択
        {
            string path = AssetDatabase.GetAssetPath(obj);
            if (path.EndsWith(".csv")) ConvertOne(path);
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/シナリオ/選択したCSVをSOに変換", true)]
    public static bool ValidateSelected()
    {
        foreach (var obj in Selection.objects)
            if (AssetDatabase.GetAssetPath(obj).EndsWith(".csv")) return true;
        return false;
    }

    // ── 入口2: フォルダ内を全部変換 ──
    [MenuItem("Tools/シナリオ/全章CSVを一括変換")]
    public static void ImportAll()
    {
        string[] files = Directory.GetFiles(CsvFolder, "*.csv");
        foreach (var path in files) ConvertOne(path.Replace('\\', '/'));
        AssetDatabase.SaveAssets();
        Debug.Log($"一括変換完了: {files.Length}章");
    }

    // ── 本体: 1ファイル分の変換(両入口から呼ばれる) ──
    private static void ConvertOne(string csvPath)
    {
        string name = Path.GetFileNameWithoutExtension(csvPath);   // "Chapter1"
        string assetPath = $"{OutputFolder}/{name}.asset";

        // パース
        var entries = new List<TextEntry>();
        var seenIds = new HashSet<string>();
        string[] rows = File.ReadAllLines(csvPath);
        for (int i = 1; i < rows.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(rows[i])) continue;
            string[] cols = rows[i].Split(',');
            string id = cols[6].Trim();
            if (!seenIds.Add(id) && id != "-" && id != "")
                Debug.LogError($"[{name}] ID重複: {id}({i + 1}行目)");
            entries.Add(new TextEntry { ID = id, Name = cols[7], JPText = cols[8] });
        }

        // 既存アセットがあれば「中身だけ更新」、なければ新規作成
        var so = AssetDatabase.LoadAssetAtPath<TextDataSO>(assetPath);
        if (so == null)
        {
            so = ScriptableObject.CreateInstance<TextDataSO>();
            so.Entries = entries;
            AssetDatabase.CreateAsset(so, assetPath);
        }
        else
        {
            so.Entries = entries;
            EditorUtility.SetDirty(so);   // 変更を保存対象としてマーク
        }
        Debug.Log($"[{name}] 変換: {entries.Count}件");
    }
}