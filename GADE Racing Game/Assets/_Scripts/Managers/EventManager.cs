using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void OnEnable() // Called before Awake
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action onCheckpointPassed;

    public void CheckpointPassed()
    {
        onCheckpointPassed?.Invoke();
    }
}
