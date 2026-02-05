using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ChoiceMenuObject = new List<GameObject>();

   
    GameObject ChoosingChoiceMenuObject;

    //public int CurrentLoadNumber;

    public static int a;

    string SceneToChange;
    void Start()
    {
        MouseMoveScript.ChosingMenuObject = ChoiceMenuObject[0];
    }

    void Update()
    {

        a = ChoiceMenuObject.IndexOf(MouseMoveScript.ChosingMenuObject);

        //â∫Å´
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Cursor.visible = false;
            IsnotMenuObject(ChoiceMenuObject.IndexOf(MouseMoveScript.ChosingMenuObject)); 

            //ëIëéàÇ™Ç§Ç≤Ç≠
            if (a + 1 < ChoiceMenuObject.Count)
            {
                MouseMoveScript.ChosingMenuObject = ChoiceMenuObject[a + 1];
                Debug.Log(a);
            }
            else if (a + 1 == ChoiceMenuObject.Count)
            {
                MouseMoveScript.ChosingMenuObject = ChoiceMenuObject[0];
                Debug.Log(a);
            }
            IsMenuObject(ChoiceMenuObject.IndexOf(MouseMoveScript.ChosingMenuObject));
        }
        
        //è„Å™
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Cursor.visible = false;
            IsnotMenuObject(ChoiceMenuObject.IndexOf(MouseMoveScript.ChosingMenuObject));

            //ëIëéàÇ™Ç§Ç≤Ç≠
            if (a == 0)
            {
                MouseMoveScript.ChosingMenuObject = ChoiceMenuObject[ChoiceMenuObject.Count - 1];
                Debug.Log(a);
            }
            else
            {
                MouseMoveScript.ChosingMenuObject = ChoiceMenuObject[a - 1];
                Debug.Log(a);
            }
            IsMenuObject(ChoiceMenuObject.IndexOf(MouseMoveScript.ChosingMenuObject));
            Debug.Log(a);
        }
        

        if (Input.GetKeyDown(KeyCode.C))
        {
            
        }


    }

    public void IsMenuObject(int a)
    {
        ChoosingChoiceMenuObject = ChoiceMenuObject[a];
        ChoosingChoiceMenuObject.SetActive(true);
    }
    public void IsnotMenuObject(int a)
    {
        ChoosingChoiceMenuObject = ChoiceMenuObject[a];
        ChoosingChoiceMenuObject.SetActive(false);
    }
}
