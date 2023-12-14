using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This class is used to determine if player wins, passes or loses the race based on how many laps the racers have.
/// </summary>
public class MultiRacerLapCounter : MonoBehaviour
{
    private int totalLaps;
    int aiRacersFinishedCount = 0;
    private LapTrackingEntity playerlapTracker;
    private List<LapTrackingEntity> aiLapTrackers;

    [SerializeField] private LapManager lapManager;
    
    [SerializeField] private EventSenderSO onRaceFinished;
    [SerializeField] private EventSenderSO onRaceLose;
    
    private void Awake()
    {
        EventManager.OnSendRacerReferences.Subscribe(ReceiveRacerReferences);
        EventManager.OnSendNumberOfLaps.Subscribe(SetTotalLaps);
        EventManager.OnDetermineIfWinRace.Subscribe(DetermineIfWinRace);
    }

    private void OnDisable()
    {
        EventManager.OnSendRacerReferences.Unsubscribe(ReceiveRacerReferences);
        EventManager.OnSendNumberOfLaps.Unsubscribe(SetTotalLaps);
        EventManager.OnDetermineIfWinRace.Unsubscribe(DetermineIfWinRace);
    }

    private void ReceiveRacerReferences(Racer[] racers)
    {
        aiLapTrackers = new List<LapTrackingEntity>(racers.Length -1);
        foreach (var racer in racers)
        {
            LapTrackingEntity trackingEntity = new LapTrackingEntity(racer.RacerID);
            if (racer is AI_Racer)
            {
                aiLapTrackers.Add(trackingEntity);
            }
            else
            {
                playerlapTracker = trackingEntity;
            }
        }
    }
    private void SetTotalLaps(int amount)
    {
        totalLaps = amount;
    }

    private void DetermineIfWinRace()
    {
        // player lap count will be equal to total laps
        
        // are all aiRacers lap counts less than total lap count
        foreach (var aiLapTracker in aiLapTrackers)
        {
            if (aiLapTracker.LapsPassed >= totalLaps)
            {
                EventManager.OnRacePassed.Invoke();
                return;
            }
        }
        onRaceFinished.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI_Racer"))
        {
            Racer racer = other.GetComponent<Racer>();
            int targetRacerId = racer.RacerID;
            foreach (var aiLapTracker in aiLapTrackers)
            {
                if (aiLapTracker.RacerId == targetRacerId)
                {
                    aiLapTracker.IncrementLapsPassed();
                    if (aiLapTracker.LapsPassed >= totalLaps)
                    {
                        aiRacersFinishedCount++;
                    }
                    break;
                }
            }
            CheckIfLose();
        }
        else if (other.CompareTag("Player"))
        {
            Racer racer = other.GetComponent<Racer>();
            playerlapTracker.LapsPassed = lapManager.currentLap;
        }
    }
    
    private void CheckIfLose()
    {
        if (aiRacersFinishedCount == aiLapTrackers.Count)
        {
            onRaceLose.Invoke();
        }
    }
}

public struct LapTrackingEntity
{
    public int LapsPassed;
    public int RacerId;

    public LapTrackingEntity(int racerId)
    {
        RacerId = racerId;
        LapsPassed = 0;
    }

    public void IncrementLapsPassed()
    {
        LapsPassed++;
    }
}
