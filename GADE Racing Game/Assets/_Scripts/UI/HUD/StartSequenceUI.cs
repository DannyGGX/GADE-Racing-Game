using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class StartSequenceUI : MonoBehaviour
{
    [SerializeField] private GameObject[] textSequence = new GameObject[3];
    [SerializeField] private StartSequence controller;

    private SoundSO beepSound;
    private void Awake()
    {
        foreach(var item in textSequence)
        {
            item.SetActive(false);
        }

    }

    public void StartReadyUp()
    {
        beepSound = SFXManager.Instance.GetSound(SoundNames.StartSequenceBeep);
        beepSound.ConfigureSource(beepSound.AudioSource);
        StartCoroutine(ReadyUpSequence());
    }

    private IEnumerator ReadyUpSequence()
    {
        WaitForSeconds wait1 = new WaitForSeconds(0.8f);
        WaitForSeconds wait2 = new WaitForSeconds(0.2f);
        for(int i = 0; i < textSequence.Length - 1; i++)
        {
            textSequence[i].SetActive(true);
            beepSound.AudioSource.pitch = 0.7f;
            beepSound.PlaySFX();
            yield return wait1;
            
            textSequence[i].SetActive(false);
            beepSound.AudioSource.pitch = 1;
            beepSound.PlaySFX();
            yield return wait2;
        }
        textSequence[textSequence.Length - 1].SetActive(true);
        controller.StartRace();
        beepSound.AudioSource.pitch = 1.3f;
        beepSound.PlaySFX();
        yield return wait1;
        
        textSequence[textSequence.Length - 1].SetActive(false);
        beepSound.AudioSource.pitch = 1;
    }
}
