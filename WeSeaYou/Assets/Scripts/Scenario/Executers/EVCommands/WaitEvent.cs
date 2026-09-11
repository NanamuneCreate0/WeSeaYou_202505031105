using System;
using UnityEngine;

public class WaitEvent : MonoBehaviour 
{
    string _currentAnim;　//常にほかのところから取得できるようにしたい
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(string id, Transform targetChara, Action onComplete)
    {
        Animator anim = targetChara.GetComponent<Animator>();
        if (!String.IsNullOrEmpty(_currentAnim)) anim.SetBool(_currentAnim, false);
        anim.SetBool(id, true);
        _currentAnim = id;
        onComplete?.Invoke();
    }
}
