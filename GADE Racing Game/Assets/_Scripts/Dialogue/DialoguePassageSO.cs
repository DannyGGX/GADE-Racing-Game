using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialoguePassage", menuName = "Scriptable Object/Dialogue/Passage")]
public class DialoguePassageSO : ScriptableObject
{
    public List<DialogueEntry> Passage = new List<DialogueEntry>();
}
