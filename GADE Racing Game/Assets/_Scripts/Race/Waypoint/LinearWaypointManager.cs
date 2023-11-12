using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearWaypointManager : WaypointManager
{
    [SerializeField] private Waypoint[] waypointsArray;

    private CustomLinkedList<Waypoint> waypointLinkedList;

    private void Awake()
    {
        BaseAwake();
        PopulateLinkedList();
    }

    protected override void HideWaypointMeshes()
    {
        foreach (var waypoint in waypointsArray)
        {
            waypoint.HideMesh();
        }
    }

    public override Node<Waypoint> GetWaypointLinkedListHead()
    {
        return waypointLinkedList.Head;
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