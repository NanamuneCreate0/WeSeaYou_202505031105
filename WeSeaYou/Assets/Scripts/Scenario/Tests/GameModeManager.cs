using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager instance { get; private set; }

    public enum GameMode
    {
        None,
        Action,
        MainScenario,
        SubScenario
    }
    public GameMode CurrentState { get; private set; }

    [SerializeField] private GameMode _firstGameMode = GameMode.None;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // シーンを跨いでもマネージャーを消したくない場合はここに追加
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // すでに存在している場合は自分を破棄する（重複防止）
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (_firstGameMode != GameMode.None)
        {
            CurrentState = _firstGameMode;
        }
        else
        {
            Debug.LogWarning("_firstGameModeが定義されていません！！");
        }
    }

    public void ChangeMode(GameMode newMode)
    {
        CurrentState = newMode;
        Debug.Log($"モードが {CurrentState} に変わったよ！");
    }

}
