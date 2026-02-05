using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(StartScenario());
        }

        // クリックされたら「View」に表示を更新させる
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextLine();
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
        if (_csvData == null || _currentIndex >= _csvData.Count || !_modeScnario_test) return;

        StartCoroutine(displayer.TypeMessage(_csvData[_currentIndex]));


        _currentIndex++;
    }
}