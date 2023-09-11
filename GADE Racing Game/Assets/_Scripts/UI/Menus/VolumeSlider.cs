using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixerGroupSO audioMixerGroup;

    private void OnEnable()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            SetAudioLevel(v);
        });
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveAllListeners();
    }

    private void Start() // Called after AudioManager initialises the mixer group volume
    {
        slider.value = audioMixerGroup.VolumeSliderValue;
    }

    public void SetAudioLevel(float value)
    {
        audioMixerGroup.SetAudioLevel(value);
    }
}
