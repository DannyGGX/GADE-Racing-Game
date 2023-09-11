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
    private bool isRaceActive = false;
    [Space]
    [SerializeField] private CarController2[] racers; // to enable and disable inputs on all racers
    private InputController[] racerInputs;
    [Space]
    [Header("Event Senders")]
    [SerializeField] private EventSenderSO onRaceReadyUp;
    [SerializeField] private EventSenderSO onRaceFinished;
    [SerializeField] private EventSenderSO onRaceLose;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        CacheRacerInputs();
        DisableRacerInputs();
        Invoke(nameof(RaceReadyUp), 0.5f);
    }

    private void FixedUpdate()
    {
        if(isRaceActive)
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
    private void EnableRacerInputs()
    {
        for(int i = 0; i < racers.Length; i++)
        {
            racers[i].InputController = racerInputs[i];
        }
    }
    private void CacheRacerInputs()
    {
        racerInputs = new InputController[racers.Length];
        for(int i = 0; i < racers.Length; i++)
        {
            racerInputs[i] = racers[i].InputController;
        }
    }

    private void RaceReadyUp()
    {
        onRaceReadyUp.Invoke();
    }

    public void StartRace() // called when race ready up is finished
    {
        isRaceActive = true;
        EnableRacerInputs();
    }

    public void HandleLapEnd()
    {
        currentLap++;
        if (currentLap > TotalLaps)
        {
            FinishRace();
        }
    }

    private void FinishRace()
    {
        onRaceFinished.Invoke();
        isRaceActive = false;
        DisableRacerInputs();
    }

    private void LoseRace()
    {
        onRaceLose.Invoke();
        isRaceActive = false;
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
            LoseRace();
            isRaceActive = false;
        }
        timerUI.UpdateTimerUI(currentTime);
    }

}
