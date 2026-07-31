using System.Collections.Generic;
using UnityEngine;

public class ScenarioLoader : MonoBehaviour
{
    [SerializeField] private TextAsset _csvFile;      // 読み込むCSVファイル
    [SerializeField] private ScenarioSystem _system; // 渡し先のManager（演出家）

    void Start()
    {
        // 1. CSVを読み込んでリストを作る（前回のコードの流用）
        List<ScenarioLine> loadedData = LoadCSV();

        // 2. 作ったリストを Manager に「はい、どうぞ！」と渡す
        _system.SetupData(loadedData);
    }

    private List<ScenarioLine> LoadCSV()
    {
        List<ScenarioLine> lineList = new List<ScenarioLine>();

        // 改行で分割
        string[] lines = _csvFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length >= 5)
            {
                ScenarioLine data = new ScenarioLine();
                data.Number = int.Parse(values[0]);
                data.Category = values[1];
                data.ID= values[2];
                data.Param = values[3];
                data.Comment = values[4];
                data.TextID = values[5];
                //data.EventWaitFlag = int.Parse(values[]);
                data.Name = values[6];
                data.JPText = values[7];
                data.ENText = values[8];
                data.CHText = values[9];
                lineList.Add(data);
            }
        }
        return lineList;
    }
}