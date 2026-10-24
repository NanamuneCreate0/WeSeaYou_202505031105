using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class SceneExcuter_End : MonoBehaviour
{
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private Animator boxAnim1a;
    [SerializeField] private Animator boxAnim1b;
    [SerializeField] private Animator boxAnim2a;
    [SerializeField] private Animator boxAnim2b;
    [SerializeField] private Animator boxAnim3a;
    [SerializeField] private Animator boxAnim3b;
    [SerializeField] private Animator thankAnim; 
    [SerializeField] private TMPro.TMP_Text youGetAnim;
    [SerializeField] private int itemID;

    private bool isInputEnabled = false;

    IDisposable anyButtonListener;

    void OnEnable()
    {
        anyButtonListener = InputSystem.onAnyButtonPress.Call(OnAnyButtonPressed);
    }

    void OnDisable()
    {
        anyButtonListener?.Dispose();
    }

    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        isInputEnabled = false;

        // アニメ1を発火
        fadeAnim.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        // IDに応じた処理
        if(PublicStaticItem.CollectionItemList.Exists(item => item.ID == 1))
        {
            Debug.Log("ID 1");
            boxAnim1a.SetTrigger("Action");
            boxAnim1b.transform.gameObject.SetActive(true);
            boxAnim1b.SetTrigger("Action");
            yield return new WaitForSeconds(0.6f);
        }
        if (PublicStaticItem.CollectionItemList.Exists(item => item.ID == 2))
        {
            Debug.Log("ID 2");
            boxAnim2a.SetTrigger("Action");
            boxAnim2b.transform.gameObject.SetActive(true);
            boxAnim2b.SetTrigger("Action");
            yield return new WaitForSeconds(0.6f);
        }
        if (PublicStaticItem.CollectionItemList.Exists(item => item.ID == 3))
        {
            Debug.Log("ID 3");
            boxAnim3a.SetTrigger("Action");
            boxAnim3b.transform.gameObject.SetActive(true);
            boxAnim3b.SetTrigger("Action");
            yield return new WaitForSeconds(0.6f);
        }


        if (PublicStaticItem.CollectionItemList.Count == 0)
        {
            youGetAnim.text = "おめでとう！次は宝も狙ってみよう！";
        }
        else if (PublicStaticItem.CollectionItemList.Count == 1)
        {
            youGetAnim.text = "宝を１つ手に入れたよ！";
        }
        else if (PublicStaticItem.CollectionItemList.Count == 2)
        {
            youGetAnim.text = "You Get 2/3 Treasure！！\nThat's Close";
        }
        else if (PublicStaticItem.CollectionItemList.Count == 3)
        {
            youGetAnim.text = "Complete！！\nYou Get All 宝　！！";
        }
        youGetAnim.color = Color.white;

        yield return new WaitForSeconds(1f);

        thankAnim.SetTrigger("Action");
        yield return new WaitForSeconds(3f);

        // 入力受付を有効化
        isInputEnabled = true;
    }
    void OnAnyButtonPressed(InputControl control)
    {
        if (isInputEnabled && control is ButtonControl)
        {
            SceneManager.LoadScene("StartScene20261018");
        }
    }
}
