using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Anim_ChikyuSkillMix : MonoBehaviour
{
    [SerializeField]
    PlayableDirector MixDirector;
    [SerializeField]
    PlayableDirector FailedDirector;
    [SerializeField]
    GameObject AnimTable0;
    [SerializeField]
    GameObject AnimTable1;
    [SerializeField]
    GameObject MixedItem;
    [SerializeField]
    Sprite transparent;
    void Start()
    {

    }
    void Update()
    {
    }
    public void StartAnim_Mix(Item item0, Item item1,Item item2)
    {
        AnimTable0.GetComponent<Image>().sprite = item0.sprite;
        AnimTable1.GetComponent<Image>().sprite = item1.sprite;
        MixedItem.GetComponent<Image>().sprite = item2.sprite;
        MixDirector.Play();
    }
    public void StartAnim_MixFailuer(Item item0, Item item1)
    {
        Debug.Log("afsdf");
        AnimTable0.GetComponent<Image>().sprite = item0.sprite;
        AnimTable1.GetComponent<Image>().sprite = item1.sprite;
        MixedItem.GetComponent<Image>().sprite = transparent;
        FailedDirector.Play();
    }
}
