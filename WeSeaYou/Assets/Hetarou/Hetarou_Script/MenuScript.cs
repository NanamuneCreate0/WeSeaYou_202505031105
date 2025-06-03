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
        if (Input.anyKeyDown && Menu == false) Menu = true;
        else if(Input.GetKeyDown(KeyCode.Escape) && Menu == true) Menu = false;

        ButtonCanvas.SetActive(Menu);
    }
}
