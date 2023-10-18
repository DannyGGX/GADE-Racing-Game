using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int WaypointId { get; set; }
    [SerializeField] private ObjEventSenderSO _objEventSenderSo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI_Racer"))
        {
            other.GetComponent<AI_Racer>().SetNextDestination();
            // send an event to PositionTracker or directly call PositionTracker and pass the racer id
            _objEventSenderSo.Invoke(5);
        }
        else if (other.CompareTag("Player"))
        {
            // send an event to PositionTracker or directly call PositionTracker and pass the racer id
        }
    }

    public void HideMesh()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
