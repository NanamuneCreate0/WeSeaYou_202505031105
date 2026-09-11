using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class CinemaScopeExecuter : MonoBehaviour, IScenarioCommand, IInputReceiver
{
    
    public string CommandType => "CINEMASCOPE";
    public bool IsAutoAdvance => false;

    [SerializeField] private CinemaScopeManager _cinemaScopeManager;
    [SerializeField] private Transform _portrayalSea;
    [SerializeField] private Transform _portrayalMare;

    public void Execute(ScenarioLine data, IScenarioContext context, Action onComplete)
    {
        StartCoroutine(PlayCoroutine(data.ID, onComplete));
    }

    private IEnumerator PlayCoroutine(string id, Action onComplete)
    {
        if (id == "CS_IN")
        {
            yield return StartCoroutine(_cinemaScopeManager.PlayOnCinemaScopeCoroutine());
            yield return new WaitForSeconds(0.75f);
            yield return StartCoroutine(PlayOnStandPortrayalCoroutine());
        }
        else if (id == "CS_OUT")
        {
            yield return StartCoroutine(_cinemaScopeManager.PlayOffCinemaScopeCoroutine());
            yield return StartCoroutine(PlayOffStandPortrayalCoroutine());
        }
        yield return new WaitForSeconds(1.5f);
        onComplete?.Invoke();
    }

    private IEnumerator PlayOnStandPortrayalCoroutine()
    {
        _portrayalSea.DOMoveX(-9f, 1f);
        _portrayalMare.DOMoveX(9f, 1f);
        yield return null;
    }

    private IEnumerator PlayOffStandPortrayalCoroutine()
    {
        _portrayalSea.DOMoveX(-15f, 1f);
        _portrayalMare.DOMoveX(15f, 1f);
        yield return null;
    }

    public void HandleAdvanceInput()
    {
        
    }
}
