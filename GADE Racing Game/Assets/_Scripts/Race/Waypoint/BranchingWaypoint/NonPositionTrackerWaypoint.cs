using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Does not send events to position tracker. That's the only difference to a normal waypoint
/// </summary>
public class NonPositionTrackerWaypoint : Waypoint
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI_Racer"))
        {
            AI_Racer aiRacer = other.GetComponent<AI_Racer>();
            aiRacer.SetNextDestination();
        }
    }
}