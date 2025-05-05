using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastTravelText_CommandExcuter : MonoBehaviour
{
    public void ExcuteCommand()
    {
        Debug.Log("ƒV[ƒ“ˆÚ“®‚·‚é‚æ‚Ë");
        SceneManager.LoadScene(gameObject.GetComponent<Text>().text);
    }
}
