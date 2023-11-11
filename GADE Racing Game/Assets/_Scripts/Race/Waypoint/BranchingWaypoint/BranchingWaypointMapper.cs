using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BranchingWaypointMapper : MonoBehaviour
{
    [SerializeField] private TrackEdge[] trackEdges;

    private Graph<Waypoint> graph;

    private Random random;

    private void Awake()
    {
        random = new Random();
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

    private CustomLinkedList<Waypoint> ConstructRandomPath(GraphNode<Waypoint> firstNode, GraphNode<Waypoint> lastNode)
    {
        // first node is the last node in a circuit race track

        CustomLinkedList<Waypoint> path = new CustomLinkedList<Waypoint>();
        path.Add(firstNode.Data);

        GraphNode<Waypoint> currentNode = firstNode;

        while (currentNode != lastNode && path.Length < 2)
        {
            // figure out which edge is ahead and which is behind
            List<Edge<Waypoint>> possibleEdges = new List<Edge<Waypoint>>();

            foreach (var edge in currentNode.Edges)
            {
                if (edge.NodeAfterEdge == currentNode) // this would mean that this edge is the edge before the current node
                    continue;
                if (edge.NodeBeforeEdge == currentNode)
                {
                    possibleEdges.Add(edge);
                }
                // Edges don't go backwards so I don't have to check that.
            }
        
            // pick random edge
            Edge<Waypoint> selectedEdge = possibleEdges[random.Next(0, possibleEdges.Count)];
            currentNode = selectedEdge.NodeAfterEdge;
            path.Add(currentNode.Data);
        }

        return path;
    }
}
