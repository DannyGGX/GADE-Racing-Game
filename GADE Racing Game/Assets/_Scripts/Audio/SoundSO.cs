using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

[CreateAssetMenu(fileName = "SoundSO", menuName = "Scriptable Object/Audio/Sound")]
public class SoundSO : ScriptableObject
{
    [SerializeField] private AudioClip clip;

    [SerializeField, Range(0f, 1f)]
    private float volume = 1;

    [SerializeField, Range(0.3f, 3f)]
    private float pitch = 1;

    [SerializeField] private bool Loop = false;

    [SerializeField, Range(0f, 1f)]
    private float spatialBlend = 0.8f;

    [Tooltip("Which mixer group to attach this sound to: Sfx or Music")]
    [SerializeField] private AudioMixerGroupSO mixerGroupSO;

    public void PlaySound(AudioSource source)
    {
        SetSource(source);
        source.Play();
    }

    public void PlaySound3D(AudioSource source, Vector3 position)
    {
        SetSource(source);
        source.Play();
    }

    private void SetSource(AudioSource source)
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = Loop;
        source.spatialBlend = spatialBlend;
        source.outputAudioMixerGroup = mixerGroupSO.MixerGroup;
    }
}
