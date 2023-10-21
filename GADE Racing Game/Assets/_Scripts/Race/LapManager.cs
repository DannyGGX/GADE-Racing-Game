using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField] private int totalLaps = 1;
    private int currentLap = 1;
    [SerializeField] private LapCounterUI lapCounterUI;
    
    [SerializeField] private EventSenderSO onRaceFinished;

    private void Awake()
    {
        lapCounterUI?.Initialize(totalLaps);
    }

    public void HandleLapEnd()
    {
        currentLap++;
        if (currentLap > totalLaps)
        {
            FinishRace();
        }
        else
        {
            lapCounterUI?.SetNextLap();
        }
    }

    private void FinishRace()
    {
        onRaceFinished.Invoke();
    }
}
