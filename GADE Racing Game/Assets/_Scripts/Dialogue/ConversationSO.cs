using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationSO", menuName = "Scriptable Object/Dialogue/Conversation")]
public class ConversationSO : ScriptableObject
{
    public List<DialogueEntry> Conversation = new List<DialogueEntry>();
}
