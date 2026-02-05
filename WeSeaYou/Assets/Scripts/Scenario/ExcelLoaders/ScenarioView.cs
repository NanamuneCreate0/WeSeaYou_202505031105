using UnityEngine;
using TMPro; // TextMeshPro用

public class ScenarioView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI messageText;

    // 「演出家」から呼ばれる命令
    public void UpdateDisplay(ScenarioLine line)
    {
        nameText.text = line.Name;
        messageText.text = line.Message;       
    }
}