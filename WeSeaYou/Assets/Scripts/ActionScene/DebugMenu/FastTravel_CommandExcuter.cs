using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastTravel_CommandExcuter : MonoBehaviour
{
    public void ExcuteCommand()
    {
        SceneManager.LoadScene(gameObject.GetComponent<Text>().text);
    }
}
