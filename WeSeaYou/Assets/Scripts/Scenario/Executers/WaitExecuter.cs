using System.Collections;
using UnityEngine;

public class WaitExecuter : MonoBehaviour, IScenarioCommand
{
    public string CommandType => "WAIT";
    public bool IsAutoAdvance => false;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Execute(ScenarioLine data, IScenarioContext context, System.Action onComplete)
    {
        // Waitコマンドの処理を実装する
        float waitTime = 0f;
        if (float.TryParse(data.Param, out waitTime))
        {
            StartCoroutine(WaitAndComplete(waitTime, onComplete));
        }
        else
        {
            Debug.LogWarning($"Invalid wait time: {data.Param}");
            onComplete?.Invoke();
        }
    }

    private IEnumerator WaitAndComplete(float waitTime, System.Action onComplete)
    {
        yield return new WaitForSeconds(waitTime);
        onComplete?.Invoke();
    }
}
