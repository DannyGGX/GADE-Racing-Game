using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Racer : Racer
{
    public AI_StatsSO RacerStats;
    
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private MeshRenderer meshRenderer;
    public WaypointTracker WaypointTracker { get; private set; }
    private void Awake()
    {
        WaypointTracker = new WaypointTracker();
    }
    private void Start()
    {
        ApplyRacerStats();
        RaceStart();
    }

    private void ApplyRacerStats()
    {
        ApplyCarMovementStats();
        AddRacerColor();
    }
    private void AddRacerColor()
    { 
        meshRenderer.material.color = RacerStats.RacerColor;
    }
    private void ApplyCarMovementStats()
    {
        navMeshAgent.acceleration = RacerStats.Acceleration;
        navMeshAgent.speed = RacerStats.Speed;
        navMeshAgent.angularSpeed = RacerStats.AngularSpeed;
        navMeshAgent.autoBraking = RacerStats.AutoBraking;
    }
    

    public void RaceStart() // Will be called after the ready, set, go
    {
        navMeshAgent.destination = WaypointTracker.GetFirstWaypointPosition();
    }

    public void SetNextDestination()
    {
        navMeshAgent.destination = WaypointTracker.GetNextWaypointPosition();
    }
}