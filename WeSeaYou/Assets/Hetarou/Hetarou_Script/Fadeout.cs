using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Fadeout : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Fade()
    {
        panel.SetActive(true);
    }
}
