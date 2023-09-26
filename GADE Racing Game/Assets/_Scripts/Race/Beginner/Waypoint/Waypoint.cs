using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int WaypointId { get; set; }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI_Racer"))
        {
            other.GetComponent<AI_Racer>().SetNextDestination();
        }
    }

    public void HideMesh()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
