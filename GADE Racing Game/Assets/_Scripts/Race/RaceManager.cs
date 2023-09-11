using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    [field: SerializeField, Range(1, 3)] public int TotalLaps { get; private set; } = 1;
    private int currentLap = 1;

    [field: Space]

    [field: SerializeField, Tooltip("Measured in seconds")] 
    public float TotalRaceTime { get; private set; }

    [field: SerializeField, Tooltip("Measured in seconds. The amount of time added when passing each checkpoint")] 
    public float CheckpointsTimeAdd { get; private set; }

    [SerializeField] private TimerUI timerUI;

    [HideInInspector] public float currentTime;
    private bool isRaceEnded = false;
    [Space]
    [SerializeField] private CarController1[] racers; // to enable and disable inputs on all racers
    [Space]
    [Header("Event Senders")]
    [SerializeField] private EventSenderSO onRaceReadyUp;
    [SerializeField] private EventSenderSO onTimeUp;
    [SerializeField] private EventSenderSO onEndRace;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DisableRacerInputs();
        Invoke(nameof(RaceReadyUp), 0.5f);
    }

    private void FixedUpdate()
    {
        if(isRaceEnded == false)
        {
            HandleCountDownTimer();
        }
    }

    private void DisableRacerInputs()
    {
        foreach (var racer in racers)
        {
            racer.InputController = new NoInput();
        }
    }

    private void RaceReadyUp()
    {
        onRaceReadyUp.Invoke();
    }

    public void HandleLapEnd()
    {
        currentLap++;
        if (currentLap > TotalLaps)
        {
            EndRace();
        }
    }

    private void EndRace()
    {
        onEndRace?.Invoke();
        isRaceEnded = true;
        DisableRacerInputs();
    }

    public void AddTime() // Called when target checkpoint is passed
    {
        currentTime += CheckpointsTimeAdd;
        timerUI.TimeAdded(CheckpointsTimeAdd);
    }

    private void HandleCountDownTimer()
    {
        currentTime -= Time.fixedDeltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            onTimeUp.Invoke();
        }
        timerUI.UpdateTimerUI(currentTime);
    }
}
