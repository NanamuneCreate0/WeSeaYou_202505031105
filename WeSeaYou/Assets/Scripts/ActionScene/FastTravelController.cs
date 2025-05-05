using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastTravelController : MonoBehaviour
{
    [SerializeField]
    GameObject MyFastTravelUI;
    [SerializeField]
    List<GameObject> FastTravelTexts=new List<GameObject>();
    [SerializeField]
    GameObject MyCursor;

    GameObject ChoosingFastTravelText;
    bool IsFastTraveling;
    string SceneToChange;
    void Start()
    {
        
    }

    void Update()
    {
        //Menu出現
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!IsFastTraveling)
            {
                IsFastTraveling = true;

                Debug.Log("FastTravelMenu");
                MyFastTravelUI.SetActive(true);

                //最初の選択肢に合わせる
                ChoosingFastTravelText=FastTravelTexts[0];
                //カーソル合わせる
                MyCursor.transform.position = ChoosingFastTravelText.transform.position;
                //行先更新
                SceneToChange = ChoosingFastTravelText.GetComponent<Text>().text;
;            }
            else if (IsFastTraveling)
            {
                IsFastTraveling = false;

                MyFastTravelUI.SetActive(false);
            }
        }


        if (IsFastTraveling)
        {
            //選択肢の仕様
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
                //行先更新
                SceneToChange = ChoosingFastTravelText.GetComponent<Text>().text;
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
                //行先更新
                SceneToChange = ChoosingFastTravelText.GetComponent<Text>().text;
            }

            //決定
            if (Input.GetKeyDown(KeyCode.O))
            {
                ExcuteCommand();
            }
        }


        //番号で簡易的にもワープできる
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneToChange = "ActionScene01";
            SceneManager.LoadScene(SceneToChange);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneToChange = "ActionScene02";
            SceneManager.LoadScene(SceneToChange);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneToChange = "ActionScene03";
            SceneManager.LoadScene(SceneToChange);
        }
    }

    void ExcuteCommand()
    {
        SceneManager.LoadScene(SceneToChange);
    }
}
