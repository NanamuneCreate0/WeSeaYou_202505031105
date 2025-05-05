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
                SetCursor();//カーソルの位置を合わせる
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
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("af");

                //選択肢がうごく
                ChoosingFastTravelText = FastTravelTexts[1];//まだ全然動かないけど、試験的に、二番目の選択肢に行く
                SetCursor();//カーソルの位置を合わせる
                SceneToChange = ChoosingFastTravelText.GetComponent<Text>().text;
            }

            //決定
            if (Input.GetKeyDown(KeyCode.O))
            {
                ChangeScene();
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

    void SetCursor()
    {
        MyCursor.transform.position = ChoosingFastTravelText.transform.position;
        //カーソルの位置うごかす
    }
}
