using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static EffekseerTool.Data.OptionValues;
using static GameModeManager;
//using static UnityEditorInternal.VersionControl.ListControl;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class CharacterReference
{
    public string NameInCSV;         // CSVに書かれる名前（例："太郎"）
    public Transform CharacterObject;// 実際のゲームオブジェクトのTransform
}

public class ScenarioManager : MonoBehaviour
{
    public enum ScenarioState
    {
        TextBox,                // テキストボックスでの文字送りや入力待ち
        SpeechBubble,           // 吹き出しでの文字送りや入力待ち
        WaitBranch              // 分岐の選択肢待ち（入力をロック）
    }
    private ScenarioState _currentState;
    private ScenarioState _lastState;

    [SerializeField] private List<CharacterReference> _characterDirectory;
    [SerializeField] private BranchCommandExecuter _branch;
    [SerializeField] private ScenarioDisplayer _displayer;
    [SerializeField] private SpeechBubbleFollower _speechBubble;

    [SerializeField] private GameObject _messageWindow;
    [SerializeField] private GameObject _cinemaScope_up;
    [SerializeField] private GameObject _cinemaScope_down;
    [SerializeField] private GameObject _still;

    private List<ScenarioLine_Test> _csvData;
    private Transform speakerTransform;
    private Image _stillImage;
    private bool _modeScnario_test = false;
    private int _currentIndex = 0;

    // Loader（読み込み担当）からデータを受け取るための入り口
    public void SetupData(List<ScenarioLine_Test> data)
    {
        _csvData = data;
        _currentIndex = 0;
        Debug.Log("データのセットアップ完了！");
    }

    void OnEnable()
    {
        // Instanceが存在するか、actionsがセットされているかを確認
        if (InputManager.Instance != null && InputManager.Instance.actions != null)
        {
            InputManager.Instance.actions.UI.AdvanceText.performed += OnScenario;
        }
        else
        {
            Debug.LogWarning("InputManagerまたはInput Actionsが準備できていません。");
        }
    }

    void OnDisable()
    {
        InputManager.Instance.actions.UI.AdvanceText.performed -= OnScenario;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump pressed");
    }

    private void Start()
    {
        _stillImage = _still.GetComponent<Image>();
    }

    void Update()
    {
        //メインイベに入ったかどうかのフラグテスト
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(StartScenario());
        }
    }

    private void OnScenario(InputAction.CallbackContext context)
    {
        if (_currentState == ScenarioState.WaitBranch)
        {
            return;
        }
        ProcessCurrentCommand();
    }

    private void ProcessCurrentCommand()
    {
        if (_csvData == null || _currentIndex >= _csvData.Count || instance.CurrentState == GameMode.Action) return;

        ScenarioLine_Test data = _csvData[_currentIndex];
        switch (data.CommandType)
        {
            case "MODECHANGE":
                if (data.Parameters == "MAIN")
                {
                    instance.ChangeMode(GameMode.MainScenario);
                }
                else if (data.Parameters == "SUB")
                {
                    instance.ChangeMode(GameMode.SubScenario);
                }
                _currentIndex++;
                ProcessCurrentCommand();
                break;

            case "TALK":
                if (!string.IsNullOrEmpty(data.Message))
                {
                    ExecuteTalkCommand(data);
                }
                break;

            case "STILL":
                ExecuteStillCommand(data);
                break;

            case "BRANCH":
                if (!string.IsNullOrEmpty(data.Parameters))
                {
                    _lastState = _currentState;
                    _currentState = ScenarioState.WaitBranch;
                    _branch.Triger(data.Message, data.Parameters, OnBranchComplete);
                    return;
                }
                break;

            case "SKIP":
                if (!string.IsNullOrEmpty(data.Parameters))
                {
                    int skipIndex = int.Parse(data.Parameters) - 1;
                    Onjump(skipIndex);
                    return;
                }
                break;

            case "END":
                if (!string.IsNullOrEmpty(data.Parameters))
                {
                    SceneManager.LoadScene(data.Parameters);
                    return;
                }
                break;
        }
    }

    private IEnumerator StartScenario()
    {
        if(_messageWindow != null) _messageWindow.SetActive(true);
        if(_cinemaScope_up != null) _cinemaScope_up.SetActive(true);
        if(_cinemaScope_down != null) _cinemaScope_down.SetActive(true);

        _modeScnario_test = true;

        yield break;
    }

    private void OnBranchComplete(int nextIndex)
    {
        _currentState = _lastState;
        Onjump(nextIndex - 1);
    }

    private void ExecuteTalkCommand(ScenarioLine_Test data)
    {
        // 1. 名簿の中から、CSVのSpeakerNameと一致するキャラクターを探す
        speakerTransform = GetCharacterTransform(data.Name);
        if (instance.CurrentState == GameMode.MainScenario)
        {
            ShowNextLineToTextBox();
        }
        else if (instance.CurrentState == GameMode.SubScenario)
        {
            if (speakerTransform != null)
            {
                // 見つかったら、その人の頭上に吹き出しを出す！
                ShowSpeechBubble(speakerTransform, data.Message);
            }
            else
            {
                // ナレーションなど、対象がいない場合は吹き出しを消すなどの処理
                HideSpeechBubble();
                // 画面下部の固定メッセージウィンドウに表示する等の分岐を入れると完璧
            }
            _currentIndex++;
        }
    }

    public void ShowSpeechBubble(Transform speakerCharacter, string message)
    {
        // 1. 生成（Instantiate）は絶対にしない！単にONにするだけ
        _speechBubble.gameObject.SetActive(true);

        // 2. 吹き出しに「今喋ってるのはこのキャラだよ」とターゲットを切り替えさせる
        _speechBubble.SetTarget(speakerCharacter);

        // 3. テキストを更新する
        _speechBubble.GetComponentInChildren<TextMeshProUGUI>().text = message;
    }
    
    public void HideSpeechBubble()
    {
        // 破壊（Destroy）は絶対にしない！OFFにして隠すだけ
        _speechBubble.gameObject.SetActive(false);
    }

    private void ShowNextLineToTextBox()
    {
        if (_csvData == null || !_modeScnario_test) return;

        ChangeCharacterColor(speakerTransform);
        if (_displayer.IsTyping)
        {
            _displayer.Skip();
            return;
        }
        else
        {
            _displayer.PlayLine(_csvData[_currentIndex]);
            _currentIndex++;
        }
    }
    private void Onjump(int jumpIndex)
    {
        if (jumpIndex >= 0)
        {
            // 進行状況の管理は総監督の仕事
            _currentIndex = jumpIndex;
        }

        // 次の行を表示
        ProcessCurrentCommand();
    }

    // あらかじめUnityのインスペクターで、Canvas内に1つだけ置いた吹き出しUIをセットしておく


    // キャラクターが喋り始めた時


    // 喋り終わった、またはUIを消す時

    private void ExecuteStillCommand(ScenarioLine_Test data)
    {
        if (string.IsNullOrEmpty(data.Parameters))
        {
            _still.SetActive(false);
        }
        else
        {
            _still.SetActive(true);
            Sprite still = Resources.Load<Sprite>("Sprites/Stills/" + data.Parameters);
            _stillImage.sprite = still;
        }

        _currentIndex++;
        ProcessCurrentCommand();
    }

    private void ChangeCharacterColor(Transform targetChara)
    {
        Debug.Log($"{targetChara}");
        foreach (var chara in _characterDirectory)
        {
            SpriteRenderer sr = chara.CharacterObject.GetComponent<SpriteRenderer>();
            if (chara.CharacterObject == targetChara)
            {
                sr.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                sr.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }
    }
    

    private Transform GetCharacterTransform(string searchName)
    {
        // 名簿が空、または検索名が空なら何もしない
        if (_characterDirectory == null || string.IsNullOrEmpty(searchName)) return null;

        // リストを1つずつチェックする
        foreach (var chara in _characterDirectory)
        {
            if (chara.NameInCSV == searchName)
            {
                return chara.CharacterObject; // 見つかったらそれを返す
            }
        }

        // 最後まで探して見つからなかったら null（空っぽ）を返す
        return null;
    }
}