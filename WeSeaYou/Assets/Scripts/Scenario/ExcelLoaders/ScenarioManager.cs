using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private ScenarioDisplayer displayer;
    [SerializeField] private GameObject _messageWindow;
    [SerializeField] private GameObject _cinemaScope_up;
    [SerializeField] private GameObject _cinemaScope_down;

    private List<ScenarioLine> _csvData;
    private bool _modeScnario_test = false;
    private int _currentIndex = 0;

    // Loader（読み込み担当）からデータを受け取るための入り口
    public void SetupData(List<ScenarioLine> data)
    {
        _csvData = data;
        _currentIndex = 0;
        Debug.Log("データのセットアップ完了！");
    }

    void Update()
    {
        //メインイベに入ったかどうかのフラグテスト
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(StartScenario());
        }

        // クリックされたら「View」に表示を更新させる
        if (Input.GetMouseButtonDown(0))
        {
            //ShowNextLine();

            ScenarioLine data = _csvData[_currentIndex];
            switch (data.CommandType)
            {
                case "END":
                    if (!string.IsNullOrEmpty(data.Parameters))
                    {
                        SceneManager.LoadScene(data.Parameters);
                        return;
                    }
                    break;

                default:
                    ShowNextLine();
                    break;


            }
        }
    }

    private IEnumerator StartScenario()
    {
        _messageWindow.SetActive(true);
        _cinemaScope_up.SetActive(true);
        _cinemaScope_down.SetActive(true);

        _modeScnario_test = true;

        yield break;
    }


    private void ShowNextLine()
    {
        if (_csvData == null || !_modeScnario_test) return;

        if (displayer.IsTyping)
        {
            displayer.Skip();
            return;
        }
        else
        {
            displayer.PlayLine(_csvData[_currentIndex]);
            _currentIndex++;
        }
    }
}