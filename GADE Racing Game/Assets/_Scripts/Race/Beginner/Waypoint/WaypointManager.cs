using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }

    [SerializeField] private Waypoint[] waypointsArray;
    [SerializeField] private bool hideWaypointMeshes = true;
    public CustomLinkedList<Waypoint> waypointLinkedList { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        
        SetWaypointsId();
        if (hideWaypointMeshes)
        {
            HideWaypointMeshes();
        }
        PopulateLinkedList();
    }

    private void SetWaypointsId()
    {
        for (int i = 0; i < waypointsArray.Length; i++)
        {
            waypointsArray[i].WaypointId = i;
        }
    }

    private void HideWaypointMeshes()
    {
        foreach (var waypoint in waypointsArray)
        {
            waypoint.HideMesh();
        }
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
