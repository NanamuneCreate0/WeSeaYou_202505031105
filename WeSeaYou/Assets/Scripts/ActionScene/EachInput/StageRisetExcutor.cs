using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageResetExcutor : MonoBehaviour
{
    [SerializeField] private Image gaugeImage;
    const float chargeTime = 1.6f;

    private float currentCharge;

    void Update()
    {
        if (InputManager.Instance.actions.Player.StageReset.IsPressed())
        {
            currentCharge += Time.deltaTime;

            // ゲージ更新
            gaugeImage.fillAmount = currentCharge / chargeTime;

            // 満タン
            if (currentCharge >= chargeTime)
            {
                currentCharge = 0f;
                gaugeImage.fillAmount = 0f;

                StageReset();
            }
        }
        else
        {
            currentCharge = 0f;
            gaugeImage.fillAmount = 0f;
        }
    }

    private void StageReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}