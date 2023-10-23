using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpectatorGroup : MonoBehaviour
{
    /// <summary>
    /// Place spectators in random order
    /// </summary>
    [SerializeField] private Spectator[] spectators;
    [Space]
    [Header("Offset Times")]
    [SerializeField, Tooltip("Time for each spectator to change to the next random state")] private float IndividualSpectatorNextAnimationOffset = 0.1f;

    [SerializeField, Tooltip("Time for each spectator to change to cheer state")]
    private float IndividualSpectatorCheerAnimationOffset = 0.08f;
    
    [Space] 
    [SerializeField] private float speedThreshold = 16;
    [Space]
    [SerializeField] private bool hideMesh;

    private int numberOfRacersInCheerArea = 0;

    private bool hasTriggeredCheering = false;

    private WaitForSeconds IndividualNextWait;
    private WaitForSeconds IndividualCheerWait;

    private MeshRenderer meshRenderer;
    
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        IndividualNextWait = new WaitForSeconds(IndividualSpectatorNextAnimationOffset);
        IndividualCheerWait = new WaitForSeconds(IndividualSpectatorCheerAnimationOffset);
        SetFirstState();
        
#if UNITY_EDITOR
        if (hideMesh)
        {
            HideMesh();
        }
        return;
#endif
        HideMesh();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (DetectIfObjectIsRacer(other))
        {
            numberOfRacersInCheerArea++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasTriggeredCheering)
            return;

        if (DetectIfObjectIsRacer(other))
        {
            if (IsSpeedAboveThreshold(GetSpeedFromRacer(other)))
            {
                StartCheering();
                hasTriggeredCheering = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (DetectIfObjectIsRacer(other))
        {
            numberOfRacersInCheerArea--;
        }

        if (hasTriggeredCheering && numberOfRacersInCheerArea == 0)
        {
            //StopCheering();
            hasTriggeredCheering = false;
        }
    }
    private void HideMesh()
    {
        meshRenderer.enabled = false;
    }

    private bool DetectIfObjectIsRacer(Collider other)
    {
        return other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI_Racer");
    }

    private float GetSpeedFromRacer(Collider other)
    {
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        return rigidbody.velocity.magnitude;
    }

    private bool IsSpeedAboveThreshold(float speed)
    {
        return speed > speedThreshold;
    }


    private void SetFirstState()
    {
        StartCoroutine(IndividuallyChangeToNextState());
    }

    private void StartCheering()
    {
        StopCoroutine(nameof(IndividuallyChangeToNextState));
        StopCoroutine(nameof(IndividuallyChangeToCheerState));
        StartCoroutine(nameof(IndividuallyChangeToCheerState));
    }

    private void StopCheering()
    {
        StopCoroutine(nameof(IndividuallyChangeToCheerState));
        StartCoroutine(nameof(IndividuallyChangeToNextState));
    }

    private IEnumerator IndividuallyChangeToCheerState()
    {
        this.Log("Set cheer state");
        
        foreach (var spectator in spectators)
        {
            spectator.SetCheerState();
            yield return IndividualCheerWait;
        }
    }

    private IEnumerator IndividuallyChangeToNextState()
    {
        this.Log("Set random state");
        
        foreach (var spectator in spectators)
        {
            spectator.SetNextState();
            yield return IndividualNextWait;
        }
    }
}
