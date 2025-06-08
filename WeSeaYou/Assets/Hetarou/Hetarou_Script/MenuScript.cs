using UnityEngine;

public class SaveButtonScript : MonoBehaviour
{
    [SerializeField]
    GameObject ButtonCanvas;

    public static bool Menu = true;
    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && Menu == false) Menu = true;
        //else if(Input.GetKeyDown(KeyCode.Escape) && Menu == true) Menu = false;

        //ButtonCanvas.SetActive(Menu);
    }
}
