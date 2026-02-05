using System;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    int TypeNumber;
    [SerializeField]
    GameObject ChoosingMenuObject;
    [SerializeField]
    GameObject AllMenuObject;

    public bool ChoosingMenu;

    public static int SameTypeNumber;
    //public static bool IsChoosingMenu = false;
    public static bool enter = false;
    GameObject StartMenuObject;

    void Start()
    {
        StartMenuObject = GameObject.Find("StartMenuObject");
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartMenuObject.GetComponent<StartMenuScript>().EndBox2();
        enter = true;
        SameTypeNumber = TypeNumber;
        Debug.Log(SameTypeNumber);
        StartMenuObject.GetComponent<StartMenuScript>().StartBox(TypeNumber);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartMenuObject.GetComponent<StartMenuScript>().EndBox1(TypeNumber);
        ChoosingMenuObject.SetActive(true);
        AllMenuObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        enter = false;
        SameTypeNumber = 100;
        StartMenuObject.GetComponent<StartMenuScript>().EndBox1(TypeNumber);
        
    }






}
