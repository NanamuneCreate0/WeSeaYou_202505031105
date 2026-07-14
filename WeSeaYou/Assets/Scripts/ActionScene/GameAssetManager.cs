using UnityEngine;

public class GameAssetManager : MonoBehaviour
{
    public static GameAssetManager Instance { get; private set; }

    [field: SerializeField]
    public GameAssets GameAssets { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}