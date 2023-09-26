using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointDetector : MonoBehaviour
{
    [SerializeField] private AI_Racer aiRacer;
    private WaypointTracker waypointTracker;
    private int currentWaypointId;

    private void Start()
    {
        waypointTracker = aiRacer.WaypointTracker;
    }

    public void SetCurrentWaypointId(int Id)
    {
        currentWaypointId = Id;
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Waypoint"))
    //     {
    //         Waypoint thisWaypoint = other.GetComponent<Waypoint>();
    //         if (thisWaypoint.WaypointId == currentWaypointId)
    //         {
    //             //Set next waypoint
    //             aiRacer.SetNextDestination();
    //             SetCurrentWaypointId(waypointTracker.GetCurrentWaypointId());
    //         }
    //     }
    // }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
        {
            Waypoint thisWaypoint = other.gameObject.GetComponent<Waypoint>();
            if (thisWaypoint.WaypointId == currentWaypointId)
            {
                //Set next waypoint
                aiRacer.SetNextDestination();
                SetCurrentWaypointId(waypointTracker.GetCurrentWaypointId());
            }
        }
    }
    
}
