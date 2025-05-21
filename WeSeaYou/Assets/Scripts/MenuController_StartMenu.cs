using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ChoiceObject = new List<GameObject>();
    [SerializeField]
    GameObject MyCursor;

    GameObject ChoosingChoiceObject;
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
        //下↓
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            //選択肢がうごく
            int a;
            a = ChoiceObject.IndexOf(ChoosingChoiceObject);
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
            int a;
            a = ChoiceObject.IndexOf(ChoosingChoiceObject);
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

        //決定
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChoosingChoiceObject.SendMessage("ExcuteCommand");
        }



    }
}
