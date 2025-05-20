using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Fadeout : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    public void Fade()
    {
        panel.SetActive(true);
    }
}
