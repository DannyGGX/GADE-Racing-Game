using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Could make this a scriptable object and have the volume slider reference it instead of having to get the mixer groups settings from AudioManager
public class AudioMixerSettings 
{
    public AudioMixerGroup SfxMixerGroup;
    public const string SFX_PARAMETER_NAME = "SfxVolume"; //Name of the audio mixer group's exposed parameter
    [field: SerializeField, Range(0.0001f, 1f)] public float SfxVolumeSliderValue { get; set; } = 0.8f;

    public AudioMixerGroup MusicMixerGroup;
    public const string MUSIC_PARAMETER_NAME = "MusicVolume";
    [field: SerializeField, Range(0.0001f, 1f)] public float MusicVolumeSliderValue { get; set; } = 0.8f;
}
