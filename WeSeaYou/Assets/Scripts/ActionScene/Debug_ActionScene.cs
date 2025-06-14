using System.Collections.Generic;
using UnityEngine;

public class Debug_ActionScene : MonoBehaviour
{
    [SerializeField]
    List<ChikyuSkillMixtureIndex> indexes =new List<ChikyuSkillMixtureIndex>();
    void Start()
    {
        for (int i = 0; i < indexes.Count; i++)
        {
            ChikyuSkillCursor.MixtureDictionary.Add(indexes[i]);
            Debug.Log(indexes[i] + " - On Index");
        }
    }
}
