using UnityEngine;
using UnityEngine.EventSystems;

public class MouseMoveScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    GameObject MenuCommandAnim;

    public static GameObject ChosingMenuObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MenuCommandAnim.SetActive(true);
        if(ChosingMenuObject != null && ChosingMenuObject != MenuCommandAnim) 
        ChosingMenuObject.SetActive(false);

        ChosingMenuObject = MenuCommandAnim;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //MenuCommandAnim.SetActive(false);
    }
}
