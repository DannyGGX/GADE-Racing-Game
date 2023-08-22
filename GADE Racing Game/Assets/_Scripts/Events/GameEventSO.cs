using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventSO", menuName = "Scriptable Object/Game Event")]
public class GameEventSO : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private List<GameEventListener> eventListeners = // List of listeners subscribed to this event. Listeners populate this array from their side
            new List<GameEventListener>();

    public void Invoke() // Invoke event to every listener
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--) // iterate backwards through the list
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void Subscribe(GameEventListener listener)
    {
        if (eventListeners.Contains(listener) == false) // Check if the list of subscribed listeners has this listener already to not add a copy
            eventListeners.Add(listener);
    }

    public void Unsubscribe(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
