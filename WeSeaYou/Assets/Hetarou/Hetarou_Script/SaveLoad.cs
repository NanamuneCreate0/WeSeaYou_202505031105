using System.Collections.Generic; // ← List<Item> を正しく使うために必要
using UnityEngine;
using System.IO;
using UnityEditor.Overlays;

public class SaveLoad : MonoBehaviour
{
    public List<Item> GetItem = PublicStaticStatus.ItemList;
    public int CurrentStageNumber = PublicStaticStatus.CurrentStage;
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/savefile.json";
        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.GetItem = GetItem;
        data.CurrentStageNumber = CurrentStageNumber;   

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("ゲームをセーブしました！");
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PublicStaticStatus.CurrentStage= data.CurrentStageNumber;

            Debug.Log("ゲームをロードしました！");
        }
        else
        {
            Debug.LogWarning("セーブデータが見つかりません！");
        }
    }
}
