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

    public int CurrentLoadNumber;

    public int a;
    //bool MyGameModeController.GameMode;
    string SceneToChange;
    void Start()
    {
        //�ŏ��̑I�����ɍ��킹��
        ChoosingChoiceObject = ChoiceObject[0];
        //�J�[�\�����킹��
        MyCursor.transform.position = ChoosingChoiceObject.transform.position;
    }

    void Update()
    {
        a = ChoiceObject.IndexOf(ChoosingChoiceObject);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetLoad(ChoiceObject.IndexOf(ChoosingChoiceObject));
        }

        //����
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            

            //�I������������
            if (a + 1 < ChoiceObject.Count)
            {
                ChoosingChoiceObject = ChoiceObject[a + 1];
                
            }
            else if (a + 1 == ChoiceObject.Count)
            {
                ChoosingChoiceObject = ChoiceObject[0];
                
            }
            //�J�[�\�����킹��
            MyCursor.transform.position = ChoosingChoiceObject.transform.position;
        }
        //�な
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //�I������������
            if (a == 0)
            {
                ChoosingChoiceObject = ChoiceObject[ChoiceObject.Count - 1];
               
            }
            else
            {
                ChoosingChoiceObject = ChoiceObject[a - 1];
                
            }
            //�J�[�\�����킹��
            MyCursor.transform.position = ChoosingChoiceObject.transform.position;

            
        }

        //����
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChoosingChoiceObject.SendMessage("ExcuteCommand");
        }

        
    }

    public void ChoosingLoadBox(int CurrentLoadBoxNumber)
    {
        ChoosingChoiceObject = ChoiceObject[CurrentLoadBoxNumber];
        MyCursor.transform.position = ChoosingChoiceObject.transform.position;
    }

    public void GetLoad(int GetLoadNumber)
    {
        Debug.Log(GetLoadNumber);
    }
}
