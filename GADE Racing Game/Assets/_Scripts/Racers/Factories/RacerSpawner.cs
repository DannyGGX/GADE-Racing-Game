using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerSpawner : MonoBehaviour
{
    [SerializeField] private RacerFactory[] racerFactories;

    private Racer[] racers;
    
    private void Awake() // Call before other script calls
    {
        SpawnAndStoreRacers();
        this.Log("Racers spawned and stored");
    }
    private void OnEnable()
    {
        SendRacers();
        this.Log("Sent racer references");
    }

    private void SpawnAndStoreRacers()
    {
        racers = new Racer[racerFactories.Length];

        for (int i = 0; i < racerFactories.Length; i++)
        {
            racers[i] = racerFactories[i].CreateRacer();
            racers[i].RacerID = i;
            racerFactories[i].ApplyStats();
        }
    }

    private void SendRacers()
    {
        EventManager.OnSendRacerReferences.Invoke(racers);
    }
    
}