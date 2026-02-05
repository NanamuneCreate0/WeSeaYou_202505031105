using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyPressKeyScript : MonoBehaviour
{
    [SerializeField]
    GameObject StartMenu;
    [SerializeField]
    GameObject AnyPressText;

    private bool Switch = true;

    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKeyDown && Switch == true)
        {
            Switch = false;
            StartMenu.SetActive(!Switch);
            AnyPressText.SetActive(Switch);
        }
    }
}
