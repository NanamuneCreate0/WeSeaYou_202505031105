using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNameDisplayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI MyTextMeshPro;
    void Start()
    {
        MyTextMeshPro.text = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
