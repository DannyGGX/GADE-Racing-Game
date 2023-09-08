using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSO", menuName = "Scriptable Object/Scene")]
public class SceneSO : ScriptableObject
{
    public Scenes sceneName;
    public int buildIndex;
}

public enum Scenes
{
    MainMenu,
    Checkpoint_Intro,
    Checkpoint_Race,
}
