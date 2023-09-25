using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }

    [SerializeField] private Waypoint[] waypointsArray;
    [field: SerializeField] public CustomLinkedList<Waypoint> waypointLinkedList { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        
        PopulateLinkedList();
    }

    private void PopulateLinkedList()
    {
        waypointLinkedList = new CustomLinkedList<Waypoint>();
        foreach (var waypoint in waypointsArray)
        {
            waypointLinkedList.Add(waypoint);
        }
    }
}
