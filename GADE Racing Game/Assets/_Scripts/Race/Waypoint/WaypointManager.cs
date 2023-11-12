using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }
    
    [SerializeField] private bool hideWaypointMeshes = true;

    protected void BaseAwake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            this.LogError("Multiple instance of WaypointManagers");
            Destroy(this);
        }

#if UNITY_EDITOR
        if (hideWaypointMeshes)
        {
            HideWaypointMeshes();
        }
        return;
#endif
        HideWaypointMeshes();
    }

    protected abstract void HideWaypointMeshes();
    
    public abstract Node<Waypoint> GetWaypointLinkedListHead();
}
