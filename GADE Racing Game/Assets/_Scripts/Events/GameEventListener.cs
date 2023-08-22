using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to subscribe to.")]
    public GameEventSO Event;

    [Tooltip("Thing to do when the event is invoked. Call the desired function from this UnityEvent.")]
    public UnityEvent Response; // UnityEvents are like serialized function calls

    private void OnEnable()
    {
        Event.Subscribe(this);
    }
    private void OnDisable()
    {
        Event.Unsubscribe(this);
    }

    public void OnEventRaised()
    {
        Response?.Invoke();
    }
}
