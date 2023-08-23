using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public CharacterSO Speaker;
    [TextArea(3,4)] public string Text;
    [Range(2, 50)] public float TextTypeSpeed = 20;
}
