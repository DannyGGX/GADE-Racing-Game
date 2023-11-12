using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private List<Waypoint> waypoints;
    
    public void CreateGraphOfWaypoints()
    {
        graph = new Graph<Waypoint>();
        waypoints = new List<Waypoint>();
        
        // add nodes to the graph before adding edges
        foreach (var trackEdge in trackEdges)
        {
            GraphNode<Waypoint> nodeBeforeEdge = new GraphNode<Waypoint>(trackEdge.WaypointBeforeEdge);
            GraphNode<Waypoint> nodeAfterEdge = new GraphNode<Waypoint>(trackEdge.WaypointAfterEdge);

            if (waypoints.Contains(nodeBeforeEdge.Data) == false)
            {
                graph.AddNode(nodeBeforeEdge);
                waypoints.Add(nodeBeforeEdge.Data);
            }
            if (waypoints.Contains(nodeAfterEdge.Data) == false)
            {
                graph.AddNode(nodeAfterEdge);
                waypoints.Add(nodeAfterEdge.Data);
            }
        }
        
        foreach (var trackEdge in trackEdges)
        {
            Edge<Waypoint> graphEdge = new Edge<Waypoint>(SearchForNode(trackEdge.WaypointBeforeEdge),
                SearchForNode(trackEdge.WaypointAfterEdge));
            graph.AddEdge(graphEdge);
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

    private GraphNode<Waypoint> SearchForNode(Waypoint waypoint)
    {
        foreach (var graphNode in graph.Nodes)
        {
            if (graphNode.Data == waypoint)
            {
                return graphNode;
            }
        }
        return new GraphNode<Waypoint>(waypoint); // I don't think it is ever supposed to return this
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
                // if (edge.NodeAfterEdge == currentNode) // this would mean that this edge is an edge before the current node
                //     continue;
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
