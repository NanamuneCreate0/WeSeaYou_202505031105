using System.Collections.Generic; // �� List<Item> �𐳂����g�����߂ɕK�v
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
        Debug.Log("�Q�[�����Z�[�u���܂����I");
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PublicStaticStatus.CurrentStage= data.CurrentStageNumber;

            Debug.Log("�Q�[�������[�h���܂����I");
        }
        else
        {
            Debug.LogWarning("�Z�[�u�f�[�^��������܂���I");
        }
    }
}
