using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LookAroundState : SpectatorState
{
    public LookAroundState()
    {
        StateID = 1;
        MinWaitTime = 5;
        MaxWaitTime = 10;
    }

    public override int EnterState()
    {
        return StateID;
    }

    public override SpectatorState Transition()
    {
        return new IdleState();
    }
}
