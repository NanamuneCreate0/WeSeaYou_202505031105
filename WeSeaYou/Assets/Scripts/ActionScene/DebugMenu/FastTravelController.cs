using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastTravelController : MonoBehaviour
{
    [SerializeField]
    GameModeController MyGameModeController;
    [SerializeField]
    GameObject MyFastTravelUI;
    [SerializeField]
    List<GameObject> FastTravelTexts=new List<GameObject>();
    [SerializeField]
    GameObject MyCursor;

    GameObject ChoosingFastTravelText;
    //bool MyGameModeController.GameMode;
    string SceneToChange;
    void Start()
    {
        
    }

    void Update()
    {
        //Menu出現
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (MyGameModeController.GameMode=="Action")
            {
                MyGameModeController.GameMode = "Debug";

                Debug.Log("FastTravelMenu");
                MyFastTravelUI.SetActive(true);

                //最初の選択肢に合わせる
                ChoosingFastTravelText=FastTravelTexts[0];
                //カーソル合わせる
                MyCursor.transform.position = ChoosingFastTravelText.transform.position;
                //行先更新
                SceneToChange = ChoosingFastTravelText.GetComponent<Text>().text;
;            }
            else if (MyGameModeController.GameMode=="Debug")
            {
                MyGameModeController.GameMode = "Action";

                MyFastTravelUI.SetActive(false);
            }
        }


        if (MyGameModeController.GameMode=="Debug")
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
            {
                //選択肢がうごく
                int a;
                a = FastTravelTexts.IndexOf(ChoosingFastTravelText);
                if (a + 1 < FastTravelTexts.Count)
                {
                    ChoosingFastTravelText = FastTravelTexts[a + 1];
                }
                else if (a + 1 == FastTravelTexts.Count)
                {
                    ChoosingFastTravelText = FastTravelTexts[0];
                }
                //カーソル合わせる
                MyCursor.transform.position = ChoosingFastTravelText.transform.position;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
            {
                //選択肢がうごく
                int a;
                a = FastTravelTexts.IndexOf(ChoosingFastTravelText);
                if (a == 0)
                {
                    ChoosingFastTravelText = FastTravelTexts[FastTravelTexts.Count - 1];
                }
                else
                {
                    ChoosingFastTravelText = FastTravelTexts[a - 1];
                }
                //カーソル合わせる
                MyCursor.transform.position = ChoosingFastTravelText.transform.position;
            }

            //決定
            if (Input.GetKeyDown(KeyCode.O))
            {
                ChoosingFastTravelText.SendMessage("ExcuteCommand");
            }
        }


        //番号で簡易的にもワープできる
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneToChange = "ActionScene01";
            ChangeScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneToChange = "ActionScene02";
            ChangeScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneToChange = "ActionScene03";
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(SceneToChange);
    }
    void SetChoice()
    {

    }
}
