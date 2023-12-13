using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }
    
    [SerializeField] private AudioMixerGroupSO sfxMixerGroup; // for initialising the mixer group volume
    [SerializeField, NotNull] private AudioCollection[] audioCollections;
    
    private HashMap<SoundNames, SoundSO> hashMap;

    private Racer[] racers; // For getting audioSource references from them.
    private List<AudioSource> aiRacerAudioSources;
    private AudioSource playerAccelerationAudioSource;
    private AudioSource playerSecondaryAudioSource;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        
        sfxMixerGroup?.InitialiseMixerGroupVolume();
        EventManager.OnSendRacerReferences.Subscribe(GetRacerAudioSources);
        PopulateHashMap();
    }

    private void OnDisable()
    {
        EventManager.OnSendRacerReferences.Unsubscribe(GetRacerAudioSources);
    }

    private void PopulateHashMap()
    {
        hashMap = new HashMap<SoundNames, SoundSO>();
        
        foreach (var audioPlayer in audioCollections)
        {
            foreach (var sound in audioPlayer.Sounds)
            {
                hashMap.Add(sound.SoundName, sound);
            }
        }
        this.Log("HashMap populated. AudioSources might need to be set before this");
    }

    private void GetRacerAudioSources(Racer[] racers)
    {
        aiRacerAudioSources = new List<AudioSource>();
        
        foreach (var racer in racers)
        {
            if (racer is AI_Racer)
            {
                aiRacerAudioSources.Add(racer.GetComponentInChildren<AudioSource>());
            }
            else if (racer is CarController3 player)
            {
                playerAccelerationAudioSource = player.GetComponentInChildren<AudioSource>();
                playerSecondaryAudioSource = player.GetComponent<AudioSource>();
            }
            
        }
    }

    public void PlaySound(SoundNames soundName)
    {
        SoundSO sound = hashMap.GetValue(soundName);
        sound.ConfigureSource(sound.AudioSource);
        sound.PlaySFX();
    }

    public void PlayLoopingSound(SoundNames soundName)
    {
        SoundSO sound = hashMap.GetValue(soundName);
        sound.ConfigureSource(sound.AudioSource);
        sound.PlaySound(); // uses AudioSource.Play() and not PlayOneShot() which doesn't loop
    }
}


