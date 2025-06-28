using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Anim_ChikyuSkillMix : MonoBehaviour
{
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
        Table0.GetComponent<Image>().sprite = item0.sprite;
        Table0.GetComponent<Image>().sprite = item1.sprite;
    }
    IEnumerator SmokeStart(Item item0, Item item1, Item item2)
    {
        /*
        Table0.SetActive(true); Table1.SetActive(true); Smoke.SetActive(true); MixedItem.SetActive(true); MixedItem.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        Table0.GetComponent<Image>().sprite = item0.sprite;
        Table1.GetComponent<Image>().sprite = item1.sprite;
        MixedItem.GetComponent<Image>().sprite = item2.sprite;

        Table0.GetComponent<Animator>().SetTrigger("Start");//Table動く
        Table1.GetComponent<Animator>().SetTrigger("Start");//Table動く
        yield return new WaitForSeconds(1);
        Smoke.GetComponent<Animator>().SetTrigger("Start");//Smok始まる
        yield return new WaitForSeconds(0.55f);//Smokeが一面を覆う
        Table0.SetActive(false); Table1.SetActive(false);//Table消える
        MixedItem.GetComponent<Image>().color = Color.white;//MixedItem出現
        Twinkle.SetActive(true);
        Twinkle.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.7f);
        Smoke.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        MixedItem.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Twinkle.SetActive(true);
        */

        Table0.SetActive(true); Table1.SetActive(true); Smoke.SetActive(true); 
        Table0.GetComponent<Image>().sprite = item0.sprite;
        Table1.GetComponent<Image>().sprite = item1.sprite;

        Table0.GetComponent<Animator>().SetTrigger("Start");//Table動く
        Table1.GetComponent<Animator>().SetTrigger("Start");//Table動く
        yield return new WaitForSeconds(1);
        Smoke.GetComponent<Animator>().SetTrigger("Start");//Smok始まる
        yield return new WaitForSeconds(0.55f);//Smokeが一面を覆う
        Table0.SetActive(false); Table1.SetActive(false);//Table消える
        MixedItem.SetActive(true);MixedItem.GetComponent<Image>().sprite = item2.sprite;//MixedItem出現
        Twinkle.SetActive(true);
        Twinkle.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.7f);
        Smoke.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        MixedItem.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Twinkle.SetActive(true);
    }
}
