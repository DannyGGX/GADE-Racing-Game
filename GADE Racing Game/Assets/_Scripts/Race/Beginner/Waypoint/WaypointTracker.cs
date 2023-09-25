using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTracker : MonoBehaviour
{
    private Node<Waypoint> currentNode;

    private void Start()
    {
        currentNode = WaypointManager.Instance.waypointLinkedList.Head;
    }

    private void SetNextNode()
    {
        currentNode = currentNode.NextNode;
    }

    public Vector3 GetCurrentWaypointPosition()
    {
        return currentNode.Data.transform.position;
    }

    public Vector3 GetNextWaypointPosition()
    {
        SetNextNode();
        return GetCurrentWaypointPosition();
    }
}
