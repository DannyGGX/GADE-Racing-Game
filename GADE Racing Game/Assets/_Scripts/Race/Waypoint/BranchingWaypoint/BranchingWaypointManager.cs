using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BranchingWaypointMapper))]
public class BranchingWaypointManager : WaypointManager
{
    [SerializeField, Tooltip("Waypoints specifically for position tracker. This is where multi-branch wide waypoints are used")] 
    private Waypoint[] positionTrackerWaypoints;

    private BranchingWaypointMapper mapper;
    /// <summary>
    /// The different routes for each of the AI racers
    /// </summary>
    private List<CustomLinkedList<Waypoint>> aiWaypointRoutes;
    
    private CustomLinkedList<Waypoint> positionTrackerRoute;

    private void Awake()
    {
        mapper = GetComponent<BranchingWaypointMapper>();
        BaseAwake(); // calls HideWaypointMeshes() which needs the mapper to access waypoints.
        mapper.CreateGraphOfWaypoints();
        aiWaypointRoutes = new List<CustomLinkedList<Waypoint>>();
    }

    protected override void HideWaypointMeshes()
    {
        foreach (var trackEdge in mapper.trackEdges)
        {
            trackEdge.WaypointBeforeEdge.HideMesh();
            trackEdge.WaypointAfterEdge.HideMesh();
        }

        foreach (var waypoint in positionTrackerWaypoints)
        {
            waypoint.HideMesh();
        }
    }

    public override Node<Waypoint> GetWaypointLinkedListHead()
    {
        aiWaypointRoutes.Add(mapper.ConstructRandomPath(out var headNode));
        return headNode;
    }

    public override Node<Waypoint> GetPositionTrackerWaypointHead()
    {
        positionTrackerRoute = PopulateLinkedList(positionTrackerWaypoints);
        return positionTrackerRoute.Head;
    }
    
    
}
