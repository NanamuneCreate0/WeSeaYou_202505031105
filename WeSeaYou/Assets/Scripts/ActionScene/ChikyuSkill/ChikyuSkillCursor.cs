using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChikyuSkillCursor : MonoBehaviour
{
    //’ˆÓ/////////////////////
    //ˆê’U‚±‚±‚ÉPublicStaticStatus”ğ“ï‚µ‚Ä‚Ü‚·
    public static int ItemVariety = 30;//‘‚«Š·‚¦‚ê‚Ä‚µ‚Ü‚¤‚ª“Ç‚İ‚İê—p
    public static List<ChikyuSkillMixtureIndex> InitialMixtureDictionary = new List<ChikyuSkillMixtureIndex>();//‘‚«Š·‚¦‚ê‚Ä‚µ‚Ü‚¤‚ª“Ç‚İ‚İê—p
    public static List<ChikyuSkillMixtureIndex> MixtureDictionary = new List<ChikyuSkillMixtureIndex>();
    //public static List<ChikyuSkillMixtureIndex> InitialMixtureDictionary_Chapter1 = new List<ChikyuSkillMixtureIndex>();//‘‚«Š·‚¦‚ê‚Ä‚µ‚Ü‚¤‚ª“Ç‚İ‚İê—p
    //public static List<ChikyuSkillMixtureIndex> MixtureDictionary_Chapter1 = new List<ChikyuSkillMixtureIndex>();

    [SerializeField]
    ChikyuSkillHand MyChikyuSkillHand;
    void Start()
    {
        
    }
    void Update()
    {
        if(MyChikyuSkillHand.isMoving==0)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                int num= (MyChikyuSkillHand.HilightStart + 2)% MyChikyuSkillHand.HandItems.Count;
                Debug.Log(MyChikyuSkillHand.HandItems[num].itemName + " Chosen");
            }
        }
    }
}
