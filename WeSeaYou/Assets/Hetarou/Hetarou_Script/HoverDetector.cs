using System;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//, IPointerClickHandler
{
    [SerializeField]
    int LoadNumber;
    [SerializeField]
    int TypeNumber;
    [SerializeField]
    GameObject ChoosingMenuObject;
    [SerializeField]
    GameObject ThisObject;
    public bool ChoosingMenu;
    public static int SameTypeNumber;
    public static bool cursor = true;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(SameTypeNumber == TypeNumber) ChoosingMenu = true;
            else if(SameTypeNumber != TypeNumber) ChoosingMenu = false;

            if (cursor == true && ChoosingMenu == false) cursor = false;
            else if (cursor == false && ChoosingMenu == false) cursor = true;
        }

        ChoosingMenuObject.SetActive(ChoosingMenu);
        ThisObject.SetActive(cursor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SameTypeNumber = TypeNumber;
        cursor = true;
        GameObject.Find("StartMenuObject").GetComponent<StartMenuScript>().StartBox(TypeNumber);

    }

    /*public void OnPointerClick(PointerEventData eventData)
    {
        if (SameTypeNumber == TypeNumber)
        {
            ChoosingMenu = true;
            ChoosingMenuObject.SetActive(ChoosingMenu);
            ThisObject.SetActive(SaveButtonScript.Menu);
        }
        
    }*/

    public void OnPointerExit(PointerEventData eventData)
    {
        SameTypeNumber = 100;
        GameObject.Find("StartMenuObject").GetComponent<StartMenuScript>().EndBox();
        if (ChoosingMenu == true) cursor = false;
    }






}
