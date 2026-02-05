using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MenuDirecter : MonoBehaviour
{
    [SerializeField]
    GameObject MenuObject;
    [SerializeField]
    GameObject PreBrackObject;
    [SerializeField]
    GameObject ButtonInMenuObject;
    [SerializeField]
    GameObject MenuBackObject;
    [SerializeField]
    GameObject Chikyu;
    [SerializeField]
    GameObject Utyu;
    //[SerializeField]
    //GameObject MenuBackObject;
    bool IsMenuObject = false;
    bool IsMenuBack = false;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(IsMenuObject == false)
            {
                IsMenuObject = true;
                MenuObject.SetActive(IsMenuObject);
                PreBrackObject.SetActive(IsMenuObject);

                IsMenuBack = false;
                MenuBack();
                
                Invoke(nameof(SetMenuButten), 0.3f);

                Chikyu.GetComponent<ChikyuWalk>().enabled = false;
                Utyu.GetComponent<UtyuWalk>().enabled = false;
            }
            else if(IsMenuObject == true)
            {
                IsMenuObject = false;
                MenuObject.SetActive(IsMenuObject);
                //PreBrackObject.SetActive(IsMenuObjec
                SetMenuButten();
                IsMenuBack = true;
                MenuBack();
                //Invoke(nameof(MenuBack), 0.3f);

                Chikyu.GetComponent<ChikyuWalk>().enabled = true;
                Utyu.GetComponent<UtyuWalk>().enabled = true;
            }
        }
    }
    void SetMenuButten()
    {
        ButtonInMenuObject.SetActive(IsMenuObject);
       
    }
    void MenuBack()
    {

        PreBrackObject.GetComponent<Animator>().SetBool("PreBrackBack", IsMenuBack);
    }
}
