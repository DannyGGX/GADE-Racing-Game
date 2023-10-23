using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LookAroundState : SpectatorState
{
    

    public override int EnterState()
    {
        
        return StateID;
    }

    public override SpectatorState Transition()
    {
        return new IdleState();
    }
}
