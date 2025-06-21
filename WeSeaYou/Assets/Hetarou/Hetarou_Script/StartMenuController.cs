using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ChoiceObject = new List<GameObject>();
    [SerializeField]
    List<GameObject> ChoiceMenuObject = new List<GameObject>();

    [SerializeField]
    GameObject AllStartMenuObject;

    GameObject ChoosingChoiceObject;

    //GameObject ChoosingChoiceMenuObject;

    public int CurrentLoadNumber;

    public int a;

    
    //bool MyGameModeController.GameMode;
    string SceneToChange;
    void Start()
    {
        //�ŏ��̑I�����ɍ��킹��
        ChoosingChoiceObject = ChoiceObject[0];
        //ChoosingChoiceMenuObject = ChoiceMenuObject[0];
        //�J�[�\�����킹��
        //MyCursor.transform.position = ChoosingChoiceObject.transform.position;
        StartBox(ChoiceObject.IndexOf(ChoosingChoiceObject));
    }

    void Update()
    {
        
        a = ChoiceObject.IndexOf(ChoosingChoiceObject);
        //HoverDetector.SameTypeNumber = a;
        //����
        //if (HoverDetector.IsChoosingMenu == false)
        
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
        Cursor.visible = false;        
        EndBox1(ChoiceObject.IndexOf(ChoosingChoiceObject));
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
            //MyCursor.transform.position = ChoosingChoiceObject.transform.position;
            StartBox(ChoiceObject.IndexOf(ChoosingChoiceObject));
            Debug.Log(a);
        }    
            //�な
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Cursor.visible = false;
            EndBox1(ChoiceObject.IndexOf(ChoosingChoiceObject));
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
            //MyCursor.transform.position = ChoosingChoiceObject.transform.position;
            StartBox(ChoiceObject.IndexOf(ChoosingChoiceObject));
            Debug.Log(a);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            EndBox1(a);
            ChoosingChoiceObject = ChoiceMenuObject[a];
            ChoosingChoiceObject.SetActive(true);
            AllStartMenuObject.SetActive(false);
        }
    }

    public void StartBox(int a)
    {
        ChoosingChoiceObject = ChoiceObject[a];
        ChoosingChoiceObject.GetComponent<Image>().color = new Color32(255, 255, 255, 125);//�F��ς���X�N���v�g
        //MyCursor.transform.position = ChoosingChoiceObject.transform.position;
        Debug.Log(a);
    }

    public void EndBox1(int a)
    {
        ChoosingChoiceObject = ChoiceObject[a];
        EndBox2();
    }

    public void EndBox2()
    {
        Color32 color32 = new(0, 0, 0, 125);
        ChoosingChoiceObject.GetComponent<Image>().color = color32;
    }

    public void ChoosingStartBox(int b)
    {
        //ChoosingChoiceObject = ChoiceObject[b];
        //ChoosingChoiceObject.SetActive(true);

        /*switch (b)
        {
            case 0:
                Debug.Log("�͂��߂���");
                break;
            case 1:
                Debug.Log("�Â�����");
                break;
            case 2:
                Debug.Log("�L��");
                break;
            case 3:
                Debug.Log("�ݒ�");
                break;
            case 4:
                Debug.Log("�I���");
                break;
        }*/
    }
}