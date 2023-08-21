using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public CharacterSO Speaker;
    [TextArea(4,4)] public string Text;
}
