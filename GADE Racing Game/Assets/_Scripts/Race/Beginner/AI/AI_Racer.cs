using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Racer : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public WaypointTracker WaypointTracker { get; private set; }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        WaypointTracker = new WaypointTracker();
    }

    private void Start()
    {
        RaceStart();
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
