using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [Space]
    [SerializeField] private AudioMixer mixer;
    private enum MixerGroups
    {
        SFX,
        Music
    }
    [SerializeField] private MixerGroups mixerGroup;

    private void OnEnable()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            SetAudioLevel(v);
        });

        if (AudioManager.Instance != null)
        {
            slider.value = GetMixerGroupVolume();
        }
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveAllListeners();
    }

    private void Start()
    {
        slider.value = GetMixerGroupVolume();

    }
    private float GetMixerGroupVolume()
    {
        if(mixerGroup == MixerGroups.SFX)
        {
            return AudioManager.Instance.SfxVolumeSliderValue;
        }
        else
        {
            return AudioManager.Instance.MusicVolumeSliderValue;
        }
    }

    public void SetAudioLevel(float value)
    {
        if (mixerGroup == MixerGroups.SFX)
        {
            mixer.SetFloat(AudioManager.SFX_PARAMETER_NAME, ToLogarithmicMixerValue(value));
            AudioManager.Instance.SfxVolumeSliderValue = value;
        }
        else
        {
            mixer.SetFloat(AudioManager.MUSIC_PARAMETER_NAME, ToLogarithmicMixerValue(value));
            AudioManager.Instance.MusicVolumeSliderValue = value;
        }
    }

    private float ToLogarithmicMixerValue(float value)
    {
        return Mathf.Log10(value) * 20;
    }
}
