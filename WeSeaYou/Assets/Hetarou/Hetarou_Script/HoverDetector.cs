using System;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    int LoadNumber;
    [SerializeField]
    GameObject ChoosingMenuObject;
    public bool ChoosingMenu;



    public void OnPointerEnter(PointerEventData eventData)
    {   
        if(LoadNumber>4)
        {
            GameObject StartMenu = GameObject.Find("StartMenuObject");
            StartMenu.GetComponent<StartMenuScript>().StartBox(LoadNumber);
            /*if (ChoosingMenu == false)*/ ChoosingMenu = true;
            //else if (ChoosingMenu == true) ChoosingMenu = false;

            ChoosingMenuObject.SetActive(ChoosingMenu);
        }
        else if(LoadNumber<5)
        {
            GameObject SaveMenu = GameObject.Find("SaveMenu");
            SaveMenu.GetComponent<StartMenuController>().ChoosingLoadBox(LoadNumber);
            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (LoadNumber > 4)
        {
            
        }
        else if (LoadNumber < 5)
        {
            GameObject SaveMenu = GameObject.Find("SaveMenu");
            SaveMenu.GetComponent<StartMenuController>().ChoosingLoadBox(LoadNumber);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (LoadNumber > 4) ChoosingMenuObject.SetActive(false);
    }



}
