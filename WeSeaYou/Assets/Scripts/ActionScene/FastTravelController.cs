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
                SetCursor();//�J�[�\���̈ʒu�����킹��
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

                //�I������������
                ChoosingFastTravelText = FastTravelTexts[1];//�܂��S�R�����Ȃ����ǁA�����I�ɁA��Ԗڂ̑I�����ɍs��
                SetCursor();//�J�[�\���̈ʒu�����킹��
                SceneToChange = ChoosingFastTravelText.GetComponent<Text>().text;
            }

            //����
            if (Input.GetKeyDown(KeyCode.O))
            {
                ChangeScene();
            }
        }


        //�ԍ��ŊȈՓI�ɂ����[�v�ł���
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
        //�J�[�\���̈ʒu��������
    }
}
