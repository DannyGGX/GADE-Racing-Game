using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BranchingWaypointMapper))]
public class BranchingWaypointManager : WaypointManager
{
    private BranchingWaypointMapper mapper;

    /// <summary>
    /// The different routes for each of the AI racers
    /// </summary>
    private List<CustomLinkedList<Waypoint>> waypointLinkedLists;

    private void Awake()
    {
        BaseAwake();
        mapper = GetComponent<BranchingWaypointMapper>();
        mapper.CreateGraphOfWaypoints();
        waypointLinkedLists = new List<CustomLinkedList<Waypoint>>();
    }

    protected override void HideWaypointMeshes()
    {
        foreach (var trackEdge in mapper.trackEdges)
        {
            trackEdge.WaypointBeforeEdge.HideMesh();
            trackEdge.WaypointAfterEdge.HideMesh();
        }
    }

    public override Node<Waypoint> GetWaypointLinkedListHead()
    {
        waypointLinkedLists.Add(mapper.ConstructRandomPath(out var headNode));
        return headNode;
    }
}
