using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerSpawner : MonoBehaviour
{
    [SerializeField] private RacerFactory[] racerFactories;
    
    private void OnEnable() // Call before other script calls
    {
        SpawnRacers();
    }

    private void SpawnRacers()
    {
        foreach (var factory in racerFactories)
        {
            factory.CreateRacer();
            factory.ApplyStats();
        }
    }
}