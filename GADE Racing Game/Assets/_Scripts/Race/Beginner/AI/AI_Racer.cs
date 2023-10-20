using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Racer : Racer
{
    public AI_StatsSO RacerStats;
    
    private NavMeshAgent navMeshAgent;
    [SerializeField] private MeshRenderer meshRenderer;
    public WaypointTracker WaypointTracker { get; private set; }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //meshRenderer = GetComponent<MeshRenderer>();
        WaypointTracker = new WaypointTracker();
        ApplyRacerStats();
    }
    private void Start()
    {
        RaceStart();
    }

    private void ApplyRacerStats()
    {
        AddRacerColor();
        ApplyCarMovementStats();
    }
    private void AddRacerColor()
    {
        //meshRenderer.material = RacerStats.BaseMaterial;
        meshRenderer.material.color = RacerStats.RacerColor;
        
    }
    private void ApplyCarMovementStats()
    {
        navMeshAgent.acceleration = RacerStats.Acceleration;
        navMeshAgent.speed = RacerStats.Speed;
        navMeshAgent.angularSpeed = RacerStats.AngularSpeed;
        navMeshAgent.autoBraking = RacerStats.AutoBraking;
    }
    

    public void RaceStart()
    {
        navMeshAgent.destination = WaypointTracker.GetFirstWaypointPosition();
    }

    public void SetNextDestination()
    {
        navMeshAgent.destination = WaypointTracker.GetNextWaypointPosition();
    }
}