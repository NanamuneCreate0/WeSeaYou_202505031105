using System;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadSlotController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    int VerticalNumber;
    [SerializeField]
    int HorizontalNumber;
    [SerializeField]
    GameObject LoaderCanvas;

    
    public int a;
    public int b; 

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        LoaderCanvas.GetComponent<StartMenuController>().EndBox();
        a = VerticalNumber;
        b = HorizontalNumber;
        LoaderCanvas.GetComponent<StartMenuController>().ChoosingLoadBox(a, b);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        a = VerticalNumber;
        b = HorizontalNumber;
        LoaderCanvas.GetComponent<StartMenuController>().ExcuteLoad(a, b);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LoaderCanvas.GetComponent<StartMenuController>().EndBox();
    }




}

