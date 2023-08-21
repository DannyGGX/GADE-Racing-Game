using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Object/Dialogue/Character")]
public class CharacterSO : ScriptableObject
{
    public string Name;
    public Sprite Avatar;
}
