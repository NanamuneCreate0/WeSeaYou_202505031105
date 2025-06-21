using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ChoiceObject = new List<GameObject>();
    [SerializeField]
    GameObject BackButton;
    [SerializeField]
    GameObject ChoosingMenuObject;
    [SerializeField]
    GameObject AllMenuObject;
    [SerializeField]
    int ThisTypeNumber;

    GameObject ChoosingChoiceObject;

    public int CurrentLoadNumber;

    public int a = 0;
    public int b = 0;
    public int[,] ChoosingNumber = new int[2, 2]
    {
        {0, 1},
        {2, 3}
    };

    void Start()
    {
        //最初の選択肢に合わせる
        ChoosingChoiceObject = ChoiceObject[0];
        //カーソル合わせる
        ChoosingLoadBox(0, 0);
    }

    void Update()
    {
        //カーソルコントローラー
        if((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && a == 0)
        {
            EndBox();
            a = 1;
            ChoosingLoadBox(a, b);
        }

        else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            EndBox();
            if(a == 1)
            {
                a = 0;
                ChoosingLoadBox(a, b);
            }

            else if(a == 0)
            {
                ChoosingChoiceObject = BackButton;
                White();
            }
        }

        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && b == 0)
        {
            EndBox();
            b = 1;
            ChoosingLoadBox(a, b);
        }

        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && b == 1)
        {
            EndBox();
            b = 0;
            ChoosingLoadBox(a, b);
        }
        //カーソル合わせる
        if(Cursor.visible == true && Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            EndBox();
            a = 0;
            b = 0;
            ChoosingLoadBox(a, b);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if (ChoosingChoiceObject != BackButton)
            {
                Debug.Log(ChoosingNumber[a, b]);
            }

            else if(ChoosingChoiceObject == BackButton)
            {
                ChoosingMenuObject.SetActive(false);
                AllMenuObject.SetActive(true);
                GameObject.Find("StartMenuObject").GetComponent<StartMenuScript>().EndBox1(ThisTypeNumber);
                GameObject.Find("StartMenuObject").GetComponent<StartMenuScript>().StartBox(0);
            }
        }
    }

    public void ChoosingLoadBox(int VerticalNumber, int HorizontalNumber)
    {
        ChoosingChoiceObject = ChoiceObject[ChoosingNumber[VerticalNumber, HorizontalNumber]];
        White();
    }

    public void EndBox()
    {
        ChoosingChoiceObject.GetComponent<Image>().color = new Color32(0, 0, 0, 125);
    }

    public void GetLoad(int VerticalNumber, int HorizontalNumber)
    {
        Debug.Log(ChoosingNumber[VerticalNumber, HorizontalNumber]);
    }

    public void White()
    {
        ChoosingChoiceObject.GetComponent<Image>().color = new Color32(255, 255, 255, 125);
    }
}
