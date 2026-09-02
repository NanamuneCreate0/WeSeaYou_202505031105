using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextData", menuName = "Scenario/TextData")]
public class TextDataSO : ScriptableObject
{
    public List<TextEntry> Entries;
}