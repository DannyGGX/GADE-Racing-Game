using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerSpawner : MonoBehaviour
{
    [SerializeField] private Factory[] racerFactories;
    
    private void OnEnable()
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