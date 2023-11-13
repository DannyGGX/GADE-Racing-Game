using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For PositionTracker to track positions in branching waypoints
/// </summary>
public class MultiBranchWideWaypoint : Waypoint
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI_Racer"))
        {
            AI_Racer aiRacer = other.GetComponent<AI_Racer>();
            EventManager.OnAIWaypointPassed.Invoke(aiRacer.RacerID);
        }
        else if (other.CompareTag("Player"))
        {
            // send an event to PositionTracker or directly call PositionTracker and pass the racer id
            EventManager.OnPlayerWaypointPassed.Invoke();
        }
    }
}