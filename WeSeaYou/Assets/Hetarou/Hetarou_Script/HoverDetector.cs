using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    int LoadNumber;
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {   
        GameObject Menu = GameObject.Find("SaveMenu");
        Menu.GetComponent<StartMenuController>().ChoosingLoadBox(LoadNumber);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject Menu = GameObject.Find("SaveMenu");
        Menu.GetComponent<StartMenuController>().GetLoad(LoadNumber);
    }

}
