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

    private int numberOfRacersWithoutWaypointRoute;

    private void Awake()
    {
        BaseAwake();
        mapper = GetComponent<BranchingWaypointMapper>();
        mapper.CreateGraphOfWaypoints();
        EventManager.OnSendRacerReferences.Subscribe(GetAmountOfRacers);
    }

    private void OnDisable()
    {
        EventManager.OnSendRacerReferences.Unsubscribe(GetAmountOfRacers);
    }

    private void GetAmountOfRacers(Racer[] racers)
    {
        numberOfRacersWithoutWaypointRoute = racers.Length;
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
        if (AreAllRoutesCreated())
            throw new NullReferenceException("All branching waypoint routes are taken");

        numberOfRacersWithoutWaypointRoute--;
        waypointLinkedLists.Add(mapper.ConstructRandomPath(out var headNode));
        return headNode;
    }

    private bool AreAllRoutesCreated()
    {
        return numberOfRacersWithoutWaypointRoute == 0;
    }
}
