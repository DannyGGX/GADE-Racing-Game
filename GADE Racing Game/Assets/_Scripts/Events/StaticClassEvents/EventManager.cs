using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for storing events in one place.
/// This event system is less resource intensive than a scriptable object event system that uses UnityEvents.
/// Con: not as good for team collaboration, because of potential conflicts from working in this class.
/// </summary>
public static class EventManager
{
    public static readonly Event OnTestEventTriggered = new Event();

    public static readonly Event<int> OnWaypointPassed = new Event<int>(); // int: racer ID
}
