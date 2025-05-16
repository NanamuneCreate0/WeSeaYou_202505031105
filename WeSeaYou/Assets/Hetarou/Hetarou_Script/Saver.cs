using UnityEngine;
using UnityEngine.SceneManagement;

public class Saver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        GameObject SaveLoad = GameObject.Find("SaveLoadmonitor");
        SaveLoad.GetComponent<SaveLoad>().SaveGame();
        Debug.Log("ÉZÅ[ÉuäÆóπ");
       
    }
}
