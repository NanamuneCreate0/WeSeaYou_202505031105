using System.Collections.Generic;
using UnityEngine;

public class ScenarioLoader : MonoBehaviour
{
    [SerializeField] private TextAsset csvFile;      // 読み込むCSVファイル
    [SerializeField] private ScenarioManager manager; // 渡し先のManager（演出家）

    void Start()
    {
        // 1. CSVを読み込んでリストを作る（前回のコードの流用）
        List<ScenarioLine> loadedData = LoadCSV();

        // 2. 作ったリストを Manager に「はい、どうぞ！」と渡す
        manager.SetupData(loadedData);
    }

    private List<ScenarioLine> LoadCSV()
    {
        List<ScenarioLine> lineList = new List<ScenarioLine>();

        // 改行で分割
        string[] lines = csvFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length >= 4)
            {
                ScenarioLine data = new ScenarioLine();
                data.Id = int.Parse(values[0]);
                data.Name = values[1];
                data.CommandType = values[2];
                data.Message = values[3];
                data.StillName = values[4];
                lineList.Add(data);
            }
        }
        return lineList;
    }
}