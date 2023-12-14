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
    public static Event<int> OnAIWaypointPassed { get; set; } = new Event<int>(); // int: racerID
    public static Event OnPlayerWaypointPassed { get; } = new Event();

    public static Event<Racer[]> OnSendRacerReferences { get; } = new Event<Racer[]>();
    public static Event<int> OnSendNumberOfLaps { get; } = new();

    public static Event OnDetermineIfWinRace { get; } = new();

    public static Event OnRacePassed { get; } = new();
}
