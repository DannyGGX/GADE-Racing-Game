using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class with an AudioSource with specific sounds to play through it
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioCollection : MonoBehaviour
{
    [NotNull] public AudioSource AudioPlayerSource;
    public SoundSO[] Sounds;

    private void Awake()
    {
        SetAudioSourceForEachSound();
    }

    private void SetAudioSourceForEachSound()
    {
        foreach (var sound in Sounds)
        {
            sound.AudioSource = AudioPlayerSource;
        }
        this.Log("SoundSOs AudioSources are set");
    }
}

