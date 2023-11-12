using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BranchingWaypointMapper : MonoBehaviour
{
    [Tooltip("Edges don't need to be in any particular order")] 
    public TrackEdge[] trackEdges;

    [SerializeField] private Waypoint FirstWaypoint;
    [SerializeField] private Waypoint lastWaypoint;

    private Graph<Waypoint> graph;

    private GraphNode<Waypoint> firstNode;
    private GraphNode<Waypoint> lastNode;

    private Random random;

    private void Awake()
    {
        random = new Random();
        firstNode = new GraphNode<Waypoint>(FirstWaypoint);
        lastNode = new GraphNode<Waypoint>(lastWaypoint);
        
        MapOutWaypoints();
    }

    private void MapOutWaypoints()
    {
        graph = new Graph<Waypoint>();

        foreach (var trackEdge in trackEdges)
        {
            GraphNode<Waypoint> nodeBeforeEdge = new GraphNode<Waypoint>(trackEdge.WaypointBeforeEdge);
            GraphNode<Waypoint> nodeAfterEdge = new GraphNode<Waypoint>(trackEdge.WaypointAfterEdge);
            trackEdge.GraphEdge = new Edge<Waypoint>(nodeBeforeEdge, nodeAfterEdge);
            
            graph.AddNode(nodeBeforeEdge);
            graph.AddNode(nodeAfterEdge);
            graph.AddEdge(trackEdge.GraphEdge);
        }
    }

    public CustomLinkedList<Waypoint> ConstructRandomPath(out Node<Waypoint> head)
    {
        CustomLinkedList<Waypoint> path = new CustomLinkedList<Waypoint>();
        path.Add(firstNode.Data);
        head = path.Head;

        GraphNode<Waypoint> currentNode = firstNode;

        while (currentNode != lastNode && path.Length < 2)
        {
            // figure out which edge is ahead and which is behind
            List<Edge<Waypoint>> possibleEdges = new List<Edge<Waypoint>>();

            foreach (var edge in currentNode.Edges)
            {
                // Edges don't go backwards so I don't have to check that.
                if (edge.NodeAfterEdge == currentNode) // this would mean that this edge is an edge before the current node
                    continue;
                if (edge.NodeBeforeEdge == currentNode)
                {
                    possibleEdges.Add(edge);
                }
            }
        
            // pick random edge
            Edge<Waypoint> selectedEdge = possibleEdges[random.Next(0, possibleEdges.Count)];
            currentNode = selectedEdge.NodeAfterEdge;
            path.Add(currentNode.Data);
        }
        return path;
    }
    
    
}
