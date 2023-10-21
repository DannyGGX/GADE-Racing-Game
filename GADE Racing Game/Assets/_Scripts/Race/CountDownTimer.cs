using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    [field: SerializeField, Tooltip("Measured in seconds")] 
    public float TotalRaceTime { get; private set; }
    
    [field: SerializeField, Tooltip("Measured in seconds. The amount of time added when passing each checkpoint")] 
    public float CheckpointsTimeAdd { get; private set; }
    
    [SerializeField] private TimerUI timerUI;
    
    [HideInInspector] public float currentTime;
    private bool isRaceActive = false;
    
    [SerializeField] private EventSenderSO onRaceLose;
    
    private void FixedUpdate()
    {
        if(isRaceActive)
        {
            HandleCountDownTimer();
        }
    }

    public void StartTime()
    {
        currentTime = TotalRaceTime;
        isRaceActive = true;
        timerUI.ShowTimer();
    }
    
    private void HandleCountDownTimer()
    {
        currentTime -= Time.fixedDeltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            LoseRace();
            isRaceActive = false;
        }
        timerUI.UpdateTimerUI(currentTime);
    }
    
    public void AddTime() // Called when target checkpoint is passed
    {
        currentTime += CheckpointsTimeAdd;
        timerUI.TimeAdded(CheckpointsTimeAdd);
    }
    
    private void LoseRace()
    {
        onRaceLose.Invoke();
        isRaceActive = false;
    }

    public void FreezeTimer() // called when race is completed
    {
        isRaceActive = false;
    }
}
