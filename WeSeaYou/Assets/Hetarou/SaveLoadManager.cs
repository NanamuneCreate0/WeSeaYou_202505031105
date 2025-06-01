using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]//�Z�[�u�������ϐ����Ƃ��������̃N���X�ɏ���
public class UserData
{
    public Vector3 savedPosition;
    public float savedHealth;
    public string savedStageName;
}

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField]
    GameObject Chikyu;
    [SerializeField]
    GameObject Utyu;
    //�������������ϐ����Đݒ�
    public static float speed = 5.0f;
    public static float health = 100.0f;
    public static string StageName;
    public Vector3 position;

    private void Start()
    {
        StageName = SceneManager.GetActiveScene().name;
        Debug.Log("���݂̃V�[����: " + StageName);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            health -= 1;
            Debug.Log(health);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnSave(1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnLoad(1);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            OnSave(2);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            OnLoad(2);
        }
    }
    public void OnSave(int slot)
    {
        UserData data = new UserData()
        {
            savedPosition = Chikyu.transform.position,
            savedHealth = health,
            savedStageName = StageName
        };


        string json = JsonUtility.ToJson(data, true);
        string key = $"PlayerUserData{slot}";
        Debug.Log(json);// json�f�[�^�ɂł������ǂ����m�F
        Debug.Log("�Z�[�u" + slot + "�ɃZ�[�u���܂���");

        //json�f�[�^�ɂ������������Ɋi�[
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public void OnLoad(int slot)
    {
        string key = $"PlayerUserData{slot}";
        if (PlayerPrefs.HasKey(key))
        {
            //json�f�[�^�ɂ�����������Ō��ɖ߂�
            string json = PlayerPrefs.GetString(key);
            UserData data = JsonUtility.FromJson<UserData>(json);

            //�����Ń��[�h
            Chikyu.transform.position = data.savedPosition;
            Utyu.transform.position = data.savedPosition;
            health = data.savedHealth;
            //SceneManager.LoadScene(data.savedStageName);
            //Debug.Log(data.savedStageName);
            Debug.Log("�Z�[�u" + slot + "�����[�h���܂���");
        }
        else
        {
            Debug.Log("PlayerUserData�����݂��܂���");
        }
    }
}