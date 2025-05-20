using UnityEngine;

public class SaveButtonScript : MonoBehaviour
{
    [SerializeField]
    GameObject ButtonCanvas;
    public bool Menu = false;
    void Start()
    {
        
    }

    void Update()
    {
        ButtonCanvas.SetActive(Menu);
    }

    public void OnSaveMenu()
    {
        Menu = true;
    }
}
