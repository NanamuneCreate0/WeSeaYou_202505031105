using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyPressKeyScript : MonoBehaviour
{
   
    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameObject director = GameObject.Find("FadeoutDirector");
            director.GetComponent<Fadeout>().Fade();
            Invoke(nameof(load), 2.0f);
        }
    }

    public void load()
    {
        SceneManager.LoadScene("StartScene02");
    }
}
