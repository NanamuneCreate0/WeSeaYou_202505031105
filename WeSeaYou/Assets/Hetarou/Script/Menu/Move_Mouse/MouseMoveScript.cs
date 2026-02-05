using UnityEngine;
using UnityEngine.EventSystems;

public class MouseMoveScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //[SerializeField]
    //GameObject MenuCommandAnim;
    [SerializeField]
    GameObject ChooseMenuObject;
    
    public static GameObject ChosingMenuCommand;
    public static GameObject ChosingMenuObject;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //MenuCommandAnim.SetActive(true);
        /*if(ChooseMenuObject != null) ChooseMenuObject.SetActive(true);

        if (ChosingMenuCommand != null && ChosingMenuCommand != MenuCommandAnim)
        {
            ChosingMenuCommand.SetActive(false);
            if(ChosingMenuObject != null)
            ChosingMenuObject.SetActive(false);
        }*/
        ChooseMenuObject.SetActive(true);
        if (ChosingMenuObject != null && ChosingMenuObject != ChooseMenuObject)
            ChosingMenuObject.SetActive(false);


        //ChosingMenuCommand = MenuCommandAnim;
        ChosingMenuObject = ChooseMenuObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //MenuCommandAnim.SetActive(false);
    }
}
