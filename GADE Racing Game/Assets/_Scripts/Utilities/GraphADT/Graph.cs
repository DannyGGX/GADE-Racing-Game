using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<T>
{
    public List<GraphNode<T>> Nodes { get; private set; }
    public List<Edge<T>> Edges { get; private set; }

    public Graph()
    {
        Nodes = new List<GraphNode<T>>();
        Edges = new List<Edge<T>>();
    }

    public void AddNode(GraphNode<T> node)
    {
        Nodes.Add(node);
    }

    public void AddEdge(GraphNode<T> nodeA, GraphNode<T> nodeB, float cost = 0)
    {
        if (Nodes.Contains(nodeA) && Nodes.Contains(nodeB))
        {
            Edges.Add(new Edge<T>(nodeA, nodeB, cost));
            Edges.Add(new Edge<T>(nodeB, nodeA, cost));
        }
    }
    
    
}
