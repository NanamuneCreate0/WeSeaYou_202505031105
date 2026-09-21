using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeathExcuter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float deathY = -10f;

    private void Update()
    {
        if (player == null) return;

        if (player.position.y <= deathY)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}