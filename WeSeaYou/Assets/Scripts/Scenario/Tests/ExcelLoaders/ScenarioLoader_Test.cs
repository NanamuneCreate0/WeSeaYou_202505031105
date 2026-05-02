using System.Collections.Generic;
using UnityEngine;

public class ScenarioLoader_Test : MonoBehaviour
{
    [SerializeField] private TextAsset csvFile;      // 読み込むCSVファイル
    [SerializeField] private ScenarioManager manager; // 渡し先のManager（演出家）

    void Start()
    {
        // 1. CSVを読み込んでリストを作る（前回のコードの流用）
        List<ScenarioLine_Test> loadedData = LoadCSV();

        // 2. 作ったリストを Manager に「はい、どうぞ！」と渡す
        manager.SetupData(loadedData);
    }

    private List<ScenarioLine_Test> LoadCSV()
    {
        List<ScenarioLine_Test> lineList = new List<ScenarioLine_Test>();

        // 改行で分割
        string[] lines = csvFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length >= 5)
            {
                ScenarioLine_Test data = new ScenarioLine_Test();
                data.Id = int.Parse(values[0]);
                data.CommandType = values[1];
                data.Name = values[2];
                data.Message = values[3];
                data.StillName = values[4];
                data.BGMName = values[5];
                data.SEName = values[6];
                data.Parameters = values[7];
                lineList.Add(data);
            }
        }
        return lineList;
    }
}