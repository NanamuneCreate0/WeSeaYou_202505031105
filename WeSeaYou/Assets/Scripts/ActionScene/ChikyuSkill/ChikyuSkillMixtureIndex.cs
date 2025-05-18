using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Index(ScObj)")]
public class ChikyuSkillMixtureIndex : ScriptableObject
{
    //ステージ毎にpossibleDictionaryが設定されている
    //このpossibleDictionaryを手元に持ってActionSceneは始まる

    //アイテムを拾う度にpossibleDictionary更新。更新とは、「その素材が引かれたDidtionary生成。満たされたDidtionaryは本Didtionaryになる」
    //上記の処理が重い場合緩和したい。つまり、上記は別スレで行われ、別スレから返答が返らない限り地球能力の窓が開かない。

    //素材選択→Didtionary更新&可素材摘出。更新とは、「その素材が引かれたDidtionary生成（その素材を含まない物ははじく）」

    public int[] RequiredMaterialsArray = new int[ChikyuSkillCursor.ItemVariety];
    public int[] RequiredMaterialsLeftArray = new int[ChikyuSkillCursor.ItemVariety];
    public Item MixtureItem;
}