using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Fadeout : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0 || mouseY != 0)
        {
            Cursor.visible = true;
        }
    }

    public void Fade()
    {
        panel.SetActive(true);
    }
}
