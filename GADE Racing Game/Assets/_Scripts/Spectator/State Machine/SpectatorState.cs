using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SpectatorState
{
    public int StateID;
    public int MinWaitTime = 1;
    public int MaxWaitTime = 10;
    
    
    public System.Random NumGenerator;

    protected SpectatorState()
    {
        NumGenerator = new System.Random();
    }
    
    /// <summary>
    /// For entering into a specific state
    /// </summary>
    /// <returns> The state ID for the animator </returns>
    public abstract int EnterState();
    
    /// <summary>
    /// Logic to run when the time has run out on another state
    /// </summary>
    /// <returns> State that is decided to change to </returns>
    public abstract SpectatorState Transition();

    public int GetStateDuration()
    {
        return NumGenerator.Next(MinWaitTime, MaxWaitTime + 1);
    }
}
