using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge<T>
{
    public GraphNode<T> NodeBeforeEdge;
    public GraphNode<T> NodeAfterEdge;
    
    /// <summary>
    /// Can be used to make a decision on which node to travel to
    /// </summary>
    public float Cost;

    public Edge(GraphNode<T> nodeBeforeEdge, GraphNode<T> nodeAfterEdge, float cost = 0)
    {
        NodeBeforeEdge = nodeBeforeEdge;
        NodeAfterEdge = nodeAfterEdge;
        Cost = cost;
        
        nodeBeforeEdge.ConnectEdge(this);
        nodeAfterEdge.ConnectEdge(this);
    }
}
