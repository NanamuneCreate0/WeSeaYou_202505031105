using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
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
        SaveLoad.GetComponent<SaveLoad>().LoadGame();
        Debug.Log("OK");
        switch (PublicStaticStatus.CurrentStage)
        {
            case 0:
                SceneManager.LoadScene("ActionScene00");
                break;

            case 1:
                SceneManager.LoadScene("ActionScene01");
                break;
        }
    }
}
