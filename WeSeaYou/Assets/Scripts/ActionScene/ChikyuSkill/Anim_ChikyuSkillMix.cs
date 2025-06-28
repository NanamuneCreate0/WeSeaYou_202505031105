using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Anim_ChikyuSkillMix : MonoBehaviour
{
    [SerializeField]
    PlayableDirector director;
    [SerializeField]
    GameObject AnimTable0;
    [SerializeField]
    GameObject AnimTable1;
    [SerializeField]
    GameObject Table0;
    [SerializeField]
    GameObject Table1;
    [SerializeField]
    GameObject MixedItem;
    [SerializeField]
    GameObject Smoke;
    [SerializeField]
    GameObject Twinkle;
    [SerializeField]
    GameObject PlusMark;

    const float AnimLength=2;
    const float LittleBack = 30/100;
    const float Pow = 2;
    const float MoveDistance=50;

    bool IsMixing;
    bool IsFailing;
    float time=0;
    void Start()
    {

    }
    void Update()
    {
    }
    public void StartAnim_Mix(Item item0, Item item1,Item item2)
    {
        StartCoroutine(SmokeStart(item0,item1,item2));
    }
    public void StartAnim_MixFailuer(Item item0, Item item1)
    {
        AnimTable0.GetComponent<Image>().sprite = item0.sprite;
        AnimTable0.GetComponent<Image>().sprite = item1.sprite;
    }
    IEnumerator SmokeStart(Item item0, Item item1, Item item2)
    {
        AnimTable0.GetComponent<Image>().sprite = item0.sprite;
        AnimTable1.GetComponent<Image>().sprite = item1.sprite;
        MixedItem.GetComponent<Image>().sprite = item2.sprite;
        yield return null;
        director.Play();
        /*
        Table0.SetActive(false); Table1.SetActive(false); AnimTable0.SetActive(true); AnimTable1.SetActive(true);
        PlusMark.SetActive(false); 
        AnimTable0.GetComponent<Image>().sprite = item0.sprite;
        AnimTable1.GetComponent<Image>().sprite = item1.sprite;

        AnimTable0.GetComponent<Animator>().SetTrigger("Start");//TableìÆÇ≠
        AnimTable1.GetComponent<Animator>().SetTrigger("Start");//TableìÆÇ≠
        yield return new WaitForSeconds(1);
        Smoke.SetActive(true); Smoke.GetComponent<Animator>().SetTrigger("Start");//SmokénÇ‹ÇÈ
        yield return new WaitForSeconds(0.55f);//SmokeÇ™àÍñ Çï¢Ç§
        AnimTable0.SetActive(false); AnimTable1.SetActive(false);//Tableè¡Ç¶ÇÈ
        MixedItem.SetActive(true);MixedItem.GetComponent<Image>().sprite = item2.sprite;//MixedItemèoåª
        Twinkle.SetActive(true);Twinkle.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.7f);
        Smoke.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        MixedItem.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Twinkle.SetActive(false);
        PlusMark.SetActive(true);
        Table0.SetActive(true); Table1.SetActive(true);
        */
    }
}
