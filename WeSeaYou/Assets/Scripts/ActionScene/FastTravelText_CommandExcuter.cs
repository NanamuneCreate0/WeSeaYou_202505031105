using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastTravelText_CommandExcuter : MonoBehaviour
{
    public void ExcuteCommand()
    {
        Debug.Log("�V�[���ړ�������");
        SceneManager.LoadScene(gameObject.GetComponent<Text>().text);
    }
}
