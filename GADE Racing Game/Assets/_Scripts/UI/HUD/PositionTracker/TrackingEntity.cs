using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEntity
{
    public int RacerID { get; set; }
    public  Node<Waypoint> TargetWaypoint { get; set; }
    public Transform RacerTransform { get; set; }
    public float DistanceToTargetWaypoint { get; set; }
    public int PassedWaypoints { get; set; } = 0; // Number of waypoints passed by this racer
    
    public bool AheadOfPlayer { get; set; }

    public TrackingEntity(int racerID, Transform racerTransform)
    {
        RacerID = racerID;
        RacerTransform = racerTransform;
    }
}
