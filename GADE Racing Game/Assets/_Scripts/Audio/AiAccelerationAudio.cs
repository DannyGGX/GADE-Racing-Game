using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AudioSource))]
public class AiAccelerationAudio : MonoBehaviour
{
    [SerializeField] private SoundSO accelerationSound;
    
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private AudioSource audioSource;
    private float accelerationValue = 0;
    
    
    
    private void OnEnable()
    {
        accelerationSound.ConfigureSource(audioSource);
        accelerationSound.PlaySound();
    }

    // Might not be necessary for AI racers since they travel mostly at constant speeds
    // private void FixedUpdate()
    // {
    //     SetAccelerationAudioValue();
    //     SetVolume();
    // }

    private void SetAccelerationAudioValue()
    {
        accelerationValue = navMeshAgent.velocity.magnitude;
    }
    
    private void SetVolume()
    {
        audioSource.volume = Mathf.Lerp(0.5f, 1f, accelerationValue);
    }
}
