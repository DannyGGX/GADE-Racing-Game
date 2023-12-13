using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for the acceleration audio for the player
/// </summary>
public class PlayerAccelerationAudio : MonoBehaviour
{
    private InputController playerInput;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    [SerializeField] private SoundSO accelerationSound;

    private float accelerationValue = 0;

    private void OnEnable()
    {
        GetPlayerInputControllerAndAudioSource();
    }

    private void GetPlayerInputControllerAndAudioSource()
    {
        playerInput = GetComponentInParent<CarController3>().InputController;
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponentInParent<Rigidbody>();
        accelerationSound.ConfigureSource(audioSource);
        accelerationSound.PlaySound();
    }

    private void FixedUpdate()
    {
        SetAccelerationAudioValue();
        SetVolume();
        //SetPitch();
    }

    private void SetAccelerationAudioValue()
    {
        accelerationValue = Mathf.Abs(playerInput.GetAccelerationInput());
    }

    private void SetVolume()
    {
        audioSource.volume = Mathf.Lerp(0.5f, 1f, accelerationValue);
    }

    private void SetPitch()
    {
        audioSource.pitch = Mathf.Lerp(-0.5f, 0.5f, accelerationValue);
    }
}
