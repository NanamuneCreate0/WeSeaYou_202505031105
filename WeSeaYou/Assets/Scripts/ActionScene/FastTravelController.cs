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
        //Menu�o��
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!IsFastTraveling)
            {
                IsFastTraveling = true;

                Debug.Log("FastTravelMenu");
                MyFastTravelUI.SetActive(true);

                //�ŏ��̑I�����ɍ��킹��
                ChoosingFastTravelText=FastTravelTexts[0];
                //�J�[�\�����킹��
                MyCursor.transform.position = ChoosingFastTravelText.transform.position;
                //�s��X�V
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
            //�I�����̎d�l
            if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
            {
                //�I������������
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
                //�J�[�\�����킹��
                MyCursor.transform.position = ChoosingFastTravelText.transform.position;
                //�s��X�V
                SceneToChange = ChoosingFastTravelText.GetComponent<Text>().text;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
            {
                //�I������������
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
                //�J�[�\�����킹��
                MyCursor.transform.position = ChoosingFastTravelText.transform.position;
                //�s��X�V
                SceneToChange = ChoosingFastTravelText.GetComponent<Text>().text;
            }

            //����
            if (Input.GetKeyDown(KeyCode.O))
            {
                ExcuteCommand();
            }
        }


        //�ԍ��ŊȈՓI�ɂ����[�v�ł���
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
