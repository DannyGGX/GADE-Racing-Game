using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode<T>
{
    public T Data { get; set; }
    
    public List<Edge<T>> Edges { get; set; }
    
    public GraphNode(T data)
    {
        Data = data;
        Edges = new List<Edge<T>>();
    }

    public void ConnectEdge(Edge<T> edge)
    {
        Edges.Add(edge);
    }
    
}
