using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IdleState : SpectatorState
{
    public IdleState()
    {
        StateID = 0;
        MinWaitTime = 5;
        MaxWaitTime = 10;
    }
    public override int EnterState()
    {
        return StateID;
    }

    public override SpectatorState Transition()
    {
        return new LookAroundState();
    }
}
