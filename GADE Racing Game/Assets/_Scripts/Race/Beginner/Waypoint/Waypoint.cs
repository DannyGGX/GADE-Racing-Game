using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [field: SerializeField] public int WaypointId { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI_Racer"))
        {
            
        }
    }
}
