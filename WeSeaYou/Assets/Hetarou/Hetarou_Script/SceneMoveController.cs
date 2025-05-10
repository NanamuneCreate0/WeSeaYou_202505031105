using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveController : MonoBehaviour
{
    [SerializeField]
    public static int CurrentStage;
    Rigidbody2D rd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            GameObject director = GameObject.Find("fade_out");
            director.GetComponent<Fadeout>().Fade();
            Invoke(nameof(load), 2.0f);
        }
    }

    public void load()
    {
        SceneManager.LoadScene("StillScene01");
    }

}
