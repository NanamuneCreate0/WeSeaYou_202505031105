using UnityEngine;

public class AnyPressKeyScript : MonoBehaviour
{
    [SerializeField]
    GameObject PressAnyKeyUI;

    public bool PressAnyKey = true;
    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKeyDown && PressAnyKey == true) PressAnyKey = false;
        else if (Input.GetKeyDown(KeyCode.Escape) && PressAnyKey == false) PressAnyKey = true;

        PressAnyKeyUI.SetActive(PressAnyKey);
    }
}
