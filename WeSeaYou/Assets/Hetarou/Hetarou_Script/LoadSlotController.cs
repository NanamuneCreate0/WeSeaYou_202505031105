using System;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadSlotController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    int LoadNumber;
    [SerializeField]
    GameObject LoaderCanvas;


    public void OnPointerEnter(PointerEventData eventData)
    {
        LoaderCanvas.GetComponent<StartMenuController>().ChoosingLoadBox(LoadNumber);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LoaderCanvas.GetComponent<StartMenuController>().GetLoad(LoadNumber);
    }





}

