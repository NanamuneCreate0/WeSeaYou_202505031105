using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ChoiceObject = new List<GameObject>();
    [SerializeField]
    GameObject MyCursor;

    GameObject ChoosingChoiceObject;

    public int CurrentLoadNumber;

    public int a;
    //bool MyGameModeController.GameMode;
    string SceneToChange;
    void Start()
    {
        //最初の選択肢に合わせる
        ChoosingChoiceObject = ChoiceObject[0];
        //カーソル合わせる
        MyCursor.transform.position = ChoosingChoiceObject.transform.position;
    }

    void Update()
    {
        a = ChoiceObject.IndexOf(ChoosingChoiceObject);
        //下↓
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {


            //選択肢がうごく
            if (a + 1 < ChoiceObject.Count)
            {
                ChoosingChoiceObject = ChoiceObject[a + 1];

            }
            else if (a + 1 == ChoiceObject.Count)
            {
                ChoosingChoiceObject = ChoiceObject[0];

            }
            //カーソル合わせる
            MyCursor.transform.position = ChoosingChoiceObject.transform.position;
        }
        //上↑
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //選択肢がうごく
            if (a == 0)
            {
                ChoosingChoiceObject = ChoiceObject[ChoiceObject.Count - 1];

            }
            else
            {
                ChoosingChoiceObject = ChoiceObject[a - 1];

            }
            //カーソル合わせる
            MyCursor.transform.position = ChoosingChoiceObject.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            ChoosingStartBox(ChoiceObject.IndexOf(ChoosingChoiceObject));
        }

  
    }

    public void StartBox(int a)
    {
        ChoosingChoiceObject = ChoiceObject[a-5];
        MyCursor.transform.position = ChoosingChoiceObject.transform.position;
    }
    public void ChoosingStartBox(int b)
    {
        switch (b)
        {
            case 0:
                Debug.Log("はじめから");
                break;
            case 1:
                Debug.Log("つづきから");
                break;
            case 2:
                Debug.Log("記憶");
                break;
            case 3:
                Debug.Log("設定");
                break;
            case 4:
                Debug.Log("終わる");
                break;
        }

    }
}