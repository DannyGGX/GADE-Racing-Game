using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField] private int totalLaps = 1;
    public int currentLap { get; private set; } = 1;
    [SerializeField] private LapCounterUI lapCounterUI;
    
    [SerializeField] private EventSenderSO onRaceFinished;
    [SerializeField] private EventSenderSO onNextLap;

    private void Start()
    {
        lapCounterUI?.Initialize(totalLaps);
        EventManager.OnSendNumberOfLaps.Invoke(totalLaps);
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
            onNextLap.Invoke();
        }
    }

    private void FinishRace()
    {
        EventManager.OnDetermineIfWinRace.Invoke();
    }
}
