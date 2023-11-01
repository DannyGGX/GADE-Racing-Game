using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge<T>
{
    public GraphNode<T> NodeA;
    public GraphNode<T> NodeB;
    
    /// <summary>
    /// Can be used to make a decision on which node to travel to
    /// </summary>
    public float Cost;

    public Edge(GraphNode<T> nodeA, GraphNode<T> nodeB, float cost = 0)
    {
        NodeA = nodeA;
        NodeB = nodeB;
        Cost = cost;
        
        nodeA.ConnectEdge(this);
        nodeB.ConnectEdge(this);
    }
}
