using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioMixerGroupSO", menuName = "Scriptable Object/Audio Mixer Group")]
public class AudioMixerGroupSO : ScriptableObject
{
    [SerializeField] private AudioMixer mixer;
    public AudioMixerGroup MixerGroup;    [SerializeField] private string exposedParameterName;
    [Range(0.0001f, 1f)] public float VolumeSliderValue = 0.8f;

    public void InitialiseMixerGroupVolume()
    {
        mixer.SetFloat(exposedParameterName, ToLogarithmicMixerValue(VolumeSliderValue));
    }
    public void SetAudioLevel(float value)
    {
        mixer.SetFloat(exposedParameterName, ToLogarithmicMixerValue(value));
        VolumeSliderValue = value;
    }
    private float ToLogarithmicMixerValue(float value)
    {
        return Mathf.Log10(value) * 20;
    }
}
