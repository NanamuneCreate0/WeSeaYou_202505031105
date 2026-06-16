using System.Collections.Generic;
using UnityEngine;

public class CharacterPortraitManager : MonoBehaviour
{
    [SerializeField] private List<CharacterPortrait> _portraits; // キャラクターポートレートのリスト
    public class CharacterPortrait
    {
        public string ID; // 例: "CHARACTER1_FACE_HAPPY"
        public Transform Image; // 顔画像やボディ画像など
    }
}
