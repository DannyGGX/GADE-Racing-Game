using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; protected set; }
    
    [SerializeField] private bool hideWaypointMeshes = true;

    protected void BaseAwake()
    {
        
        
        //SetWaypointsId();
        
#if UNITY_EDITOR
        if (hideWaypointMeshes)
        {
            HideWaypointMeshes();
        }
        return;
#endif
        HideWaypointMeshes();
    }

    // private void SetWaypointsId()
    // {
    //     for (int i = 0; i < waypointsArray.Length; i++)
    //     {
    //         waypointsArray[i].WaypointId = i;
    //     }
    // }

    protected abstract void HideWaypointMeshes();
    
    public abstract Node<Waypoint> GetWaypointLinkedListHead();
}
