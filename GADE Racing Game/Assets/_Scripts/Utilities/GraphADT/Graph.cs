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

    public void AddEdge(GraphNode<T> nodeBeforeEdge, GraphNode<T> nodeAfterEdge, float cost = 0)
    {
        if (Nodes.Contains(nodeBeforeEdge) && Nodes.Contains(nodeAfterEdge))
        {
            Edges.Add(new Edge<T>(nodeBeforeEdge, nodeAfterEdge, cost));
            //Edges.Add(new Edge<T>(nodeAfterEdge, nodeBeforeEdge, cost)); //don't need to go backwards
        }
    }

    public void AddEdge(Edge<T> edge)
    {
        Edges.Add(edge);
    }
    
    
}
