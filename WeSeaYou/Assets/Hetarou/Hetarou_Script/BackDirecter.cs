using UnityEngine;
using UnityEngine.EventSystems;

public class BackDirecter : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{

    [SerializeField]
    GameObject ChoosingMenuObject;
    [SerializeField]
    GameObject AllMenuObject;
    [SerializeField]
    int ThisTypeNumber;
    
    void Start()
    {
        
    }

    void Update()
    {
        /*ChoosingMenuObject.SetActive(HoverDetector.IsChoosingMenu);
        AllMenuObject.SetActive(!HoverDetector.IsChoosingMenu);*/
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //HoverDetector.SameTypeNumber = 100;
        //Debug.Log(HoverDetector.SameTypeNumber);
        //HoverDetector.IsChoosingMenu = false;
        ChoosingMenuObject.SetActive(false);
        AllMenuObject.SetActive(true);
        GameObject.Find("StartMenuObject").GetComponent<StartMenuScript>().EndBox1(ThisTypeNumber);
        GameObject.Find("StartMenuObject").GetComponent<StartMenuScript>().StartBox(0);
    }
}
