using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro; // Actionを使うために必要

public class BranchCommandExecuter : MonoBehaviour
{
    [SerializeField] private GameObject _branchPanel;
    [SerializeField] private Button[] _branchButtons;

    private Action<int> _onBranchSelected;
    public void Triger(string content, string param, Action<int> onComplete)
    {
        _branchPanel.SetActive(true);
        _onBranchSelected = onComplete;

        string[] branchTextValues = content.Split('/');
        string[] jumpToIDValues = param.Split('/');

        for (int i = 0; i < _branchButtons.Length; i++)
        {
            if (i < branchTextValues.Length && i < jumpToIDValues.Length)
            {
                _branchButtons[i].gameObject.SetActive(true);
                // テキストのセット（ボタンの子オブジェクトにTextがある前提）
                _branchButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = branchTextValues[i];

                string jumpTarget = jumpToIDValues[i];
                _branchButtons[i].onClick.RemoveAllListeners();
                _branchButtons[i].onClick.AddListener(() => OnSelectBranch(jumpTarget));
            }
            else
            {
                _branchButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnSelectBranch(string jumpTarget)
    {
        _branchPanel.SetActive(false);

        if (int.TryParse(jumpTarget, out int nextIndex))
        {
            for (int i = 0; i < _branchButtons.Length; i++)
            {
                _branchButtons[i].onClick.RemoveAllListeners();
                _branchButtons[i].gameObject.SetActive(false);
            }

            // 自分で進行するのではなく、総監督に「nextIndex」を報告する！
            _onBranchSelected?.Invoke(nextIndex);
        }
        else
        {
            Debug.LogError("ジャンプ先のパースに失敗しました: " + jumpTarget);
            // 失敗した場合のフォールバック（とりあえず次の行へなど）
            _onBranchSelected?.Invoke(-1);
        }
    }


}
