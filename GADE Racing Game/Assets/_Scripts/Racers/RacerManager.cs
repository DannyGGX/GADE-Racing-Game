using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerManager : MonoBehaviour
{
    public static RacerManager Instance { get; private set; }

    [SerializeField] private Racer[] racers; // to enable and disable inputs on all racers

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        
        EventManager.OnSendRacerReferences.Subscribe(ReceiveAndStoreRacers);
        this.Log("Subscribed to receive racer references");
    }

    private void Start()
    {
        ApplyAIAttributes();
        DisableRacerInputs();
        this.Log("Call upon racer inputs to be disabled");
    }
    private void OnDisable()
    {
        EventManager.OnSendRacerReferences.Unsubscribe(ReceiveAndStoreRacers);
    }

    private void ReceiveAndStoreRacers(Racer[] racers)
    {
        this.racers = new Racer[racers.Length];
        this.racers = racers;
        this.Log($"Received Racers: {racers[0].RacerID}, {racers[1].RacerID}, {racers[2].RacerID}, {racers[3].RacerID}");
    }

    private void ApplyAIAttributes() // Because AI script is disabled before this code can run on their script
    {
        foreach (var racer in racers)
        {
            if (racer is AI_Racer aiRacer)
            {
                aiRacer.ApplyRacerStats(); // Make sure they are applied in case this Start ran before AI_Racer Start
            }
        }
    }

    private void DisableRacerInputs()
    {
        foreach (var racer in racers)
        {
            if (racer is CarController2 player)
            {
                player.InputController = new NoInput();
            }
            else // if racer is AI
            {
                racer.enabled = false;
            }
        }
    }

    
    private void EnableRacerInputs()
    {
        foreach (var racer in racers)
        {
            if (racer is CarController2 player)
            {
                player.InputController = new PlayerInput();
            }
            else // if racer is AI
            {
                AI_Racer aiRacer = (AI_Racer)racer;
                aiRacer.enabled = true;
                aiRacer.SetFirstDestination();
            }
        }
    }

    public void StartRace() // called when race ready up is finished
    {
        EnableRacerInputs();
        this.Log("Call upon racer inputs to be enabled");
    }


    public void FinishRace() // called on last lap when last checkpoint is reached
    {
        DisableRacerInputs();
    }

    public void LoseRace() // called when ran out of time or all other racers passed last waypoint on their last lap
    {
        DisableRacerInputs();
    }

}
