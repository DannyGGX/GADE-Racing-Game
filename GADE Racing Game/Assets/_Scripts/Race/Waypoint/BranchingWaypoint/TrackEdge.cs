using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class TrackEdge : MonoBehaviour
{
    public Edge<Waypoint> GraphEdge;
    public Waypoint WaypointBeforeEdge;
    public Waypoint WaypointAfterEdge;

    private void OnDrawGizmos()
    {
        if (WaypointBeforeEdge == null || WaypointAfterEdge == null) return;
        
        // draw connector line
        Gizmos.color = Color.green;
        Vector3 pointBeforeLine = WaypointBeforeEdge.transform.position;
        pointBeforeLine.y += 10;
        Vector3 pointAfterLine = WaypointAfterEdge.transform.position;
        pointAfterLine.y += 10;
        Gizmos.DrawLine(pointBeforeLine, pointAfterLine);
        
        // // draw direction indication line. From midpoint to point after line.
        // Vector3 midpoint = CalculateMidpoint(pointBeforeLine, pointAfterLine);
        //
        // midpoint.y += 2;
        // Gizmos.color = Color.red;
        // Gizmos.DrawLine(midpoint, pointAfterLine);
        
        // Direction indication arrow
        Vector3 triangleTopPoint = GetPointOnLine(pointAfterLine, pointBeforeLine, 10);
        triangleTopPoint.y += 1;
        Vector3 triangleBottomPoint = triangleTopPoint;
        triangleTopPoint.y -= 2;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(triangleTopPoint, pointAfterLine);
        Gizmos.DrawLine(triangleBottomPoint, pointAfterLine);
        Gizmos.DrawLine(triangleTopPoint, triangleBottomPoint);
    }
    
    private Vector3 CalculateMidpoint(Vector3 point1, Vector3 point2)
    {
        float xMidpoint = (point1.x + point2.x) / 2f;
        float yMidpoint = (point1.y + point2.y) / 2f;
        float zMidpoint = (point1.z + point2.z) / 2f;

        Vector3 midpoint = new Vector3(xMidpoint, yMidpoint, zMidpoint);

        return midpoint;
    }
    
    private Vector3 GetPointOnLine(Vector3 linePoint1, Vector3 linePoint2, float distanceFromPoint1) // the line is made from point1 to point2
    {
        // Calculate the direction vector of the line
        Vector3 lineDirection = (linePoint2 - linePoint1).normalized;

        // Calculate the point on the line at the specified distance from linePoint1
        Vector3 pointOnLine = linePoint1 + lineDirection * distanceFromPoint1;

        return pointOnLine;
    }
}