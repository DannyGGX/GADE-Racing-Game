using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public SoundNames Name;

    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 1;

    [Range(0.3f, 3f)]
    public float Pitch = 1;

    public bool Loop = false;

    [Range(0f, 1f)]
    public float SpatialBlend = 0.8f;

    public SoundTypes SoundType;

    [HideInInspector] public AudioSource audioSource;
}

public enum SoundTypes // To show which audio mixer to play from
{
    SFX,
    Music,
}
public enum SoundNames
{ 
    Vroom,
    Skkkr,

    MenuMusic,
    CkeckpointRaceMusic,
}