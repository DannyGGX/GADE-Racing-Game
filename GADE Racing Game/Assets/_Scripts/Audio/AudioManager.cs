using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixerGroupSO sfxMixerGroup;
    [SerializeField] private AudioMixerGroupSO musicMixerGroup;
    [field: Space]
    [field: SerializeField] public Sound[] Sounds { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);

        InitialiseAudioMixer();
        InitialiseSounds();
    }
    private void InitialiseSounds()
    {
        foreach (Sound sound in Sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.Clip;
            sound.audioSource.volume = sound.Volume;
            sound.audioSource.pitch = sound.Pitch;
            sound.audioSource.loop = sound.Loop;
            if (sound.SoundType == SoundTypes.SFX)
            {
                sound.audioSource.outputAudioMixerGroup = sfxMixerGroup.MixerGroup;
            }
            else
            {
                sound.audioSource.outputAudioMixerGroup = musicMixerGroup.MixerGroup;
            }
        }
    }
    private void InitialiseAudioMixer()
    {
        sfxMixerGroup.InitialiseMixerGroupVolume();
        musicMixerGroup.InitialiseMixerGroupVolume();
    }

    private Sound GetSound(SoundNames name)
    {
        Sound sound = Array.Find(Sounds, s => s.Name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return null;
        }
        return sound;
    }

    public void PlaySoundEffect(SoundNames name)
    {
        Sound sound = GetSound(name);
        if (sound != null)
            sound.audioSource.PlayOneShot(sound.audioSource.clip);
    }
    public void PlaySound3D(SoundNames name, Vector3 position)
    {
        Sound sound = GetSound(name);
        if (sound != null)
        {
            GameObject soundObject = new GameObject("3DSound");
            soundObject.transform.position = position;
            AudioSource audioSource3D = sound.audioSource;
            audioSource3D = soundObject.AddComponent<AudioSource>();
        }
    }
    public void PlayMusic(SoundNames name)
    {
        Sound sound = Array.Find(Sounds, s => s.Name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }

        if (sound.audioSource.isPlaying)
        {
            return;
        }
        else
        {
            sound.audioSource.Play();
        }
    }

    public void StopSound(SoundNames name)
    {
        Sound sound = Array.Find(Sounds, s => s.Name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }

        sound.audioSource.Stop();

        //if (sound.audioSource.isPlaying)
        //{
        //    sound.audioSource.Stop();
        //}
    }

    public void StopAllSounds()
    {
        foreach (var sound in Sounds)
        {
            sound.audioSource?.Stop();
        }
    }


    public bool IsMenuMusicPlaying()
    {
        Sound sound = Array.Find(Sounds, s => s.Name == SoundNames.MenuMusic);
        if (sound == null)
        {
            return false;
        }
        return sound.audioSource.isPlaying;
    }

    public bool IsSoundPlaying(SoundNames name)
    {
        Sound sound = Array.Find(Sounds, s => s.Name == name);
        if (sound == null)
        {
            return false;
        }
        return sound.audioSource.isPlaying;
    }
}
