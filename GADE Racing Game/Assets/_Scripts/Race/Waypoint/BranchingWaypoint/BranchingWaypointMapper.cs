using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BranchingWaypointMapper : MonoBehaviour
{
    [Tooltip("Edges don't need to be in any particular order")] 
    public TrackEdge[] trackEdges;

    [FormerlySerializedAs("FirstWaypoint")] [SerializeField] private Waypoint firstWaypoint;
    
    [SerializeField, Tooltip("Waypoint just before first waypoint in a looping track")] 
    private Waypoint lastWaypoint;

    private Graph<Waypoint> graph;
    private GraphNode<Waypoint> firstNode;
    private GraphNode<Waypoint> lastNode;
    
    public void CreateGraphOfWaypoints()
    {
        graph = new Graph<Waypoint>();

        foreach (var trackEdge in trackEdges)
        {
            GraphNode<Waypoint> nodeBeforeEdge = new GraphNode<Waypoint>(trackEdge.WaypointBeforeEdge);
            GraphNode<Waypoint> nodeAfterEdge = new GraphNode<Waypoint>(trackEdge.WaypointAfterEdge);
            Edge<Waypoint> graphEdge = new Edge<Waypoint>(nodeBeforeEdge, nodeAfterEdge);
            
            graph.AddNode(nodeBeforeEdge);
            graph.AddNode(nodeAfterEdge);
            graph.AddEdge(graphEdge);
            
            // // set first and last node
            // if (nodeBeforeEdge.Data == firstWaypoint)
            // {
            //     firstNode = nodeBeforeEdge;
            // }
            //
            // if (nodeAfterEdge.Data == lastWaypoint)
            // {
            //     lastNode = nodeAfterEdge;
            // }
        }
        
        // set first and last node
        foreach (var graphNode in graph.Nodes)
        {
            if (graphNode.Data == firstWaypoint)
            {
                firstNode = graphNode;
            }
            else if (graphNode.Data == lastWaypoint)
            {
                lastNode = graphNode;
            }
        }
        
        this.Log("Graph created");
    }
    
    public CustomLinkedList<Waypoint> ConstructRandomPath(out Node<Waypoint> head)
    {
        CustomLinkedList<Waypoint> path = new CustomLinkedList<Waypoint>();
        path.Add(firstNode.Data);

        GraphNode<Waypoint> currentNode = firstNode;

        while (currentNode != lastNode)
        {
            // check what edge(s) are ahead and what edge(s) are behind. possibleEdges are the edge(s) that are ahead.
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
            Edge<Waypoint> selectedEdge = possibleEdges[Random.Range(0, possibleEdges.Count)];
            currentNode = selectedEdge.NodeAfterEdge;
            path.Add(currentNode.Data);
        }
        head = path.Head;
        return path;
    }
    
}
