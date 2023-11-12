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
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        
        BaseAwake();
        mapper = GetComponent<BranchingWaypointMapper>();
        EventManager.OnSendRacerReferences.Subscribe(GetAmountOfRacers);
    }

    private void OnDisable()
    {
        EventManager.OnSendRacerReferences.Unsubscribe(GetAmountOfRacers);
    }

    private void GetAmountOfRacers(Racer[] racers)
    {
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

        waypointLinkedLists.Add(mapper.ConstructRandomPath(out var headNode));
        return headNode;
    }

    private bool AreAllRoutesCreated()
    {
        return waypointLinkedLists[^1] != null; // ^1 means index end
    }
}
