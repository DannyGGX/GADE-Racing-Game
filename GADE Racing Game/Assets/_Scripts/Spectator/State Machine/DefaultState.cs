using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : SpectatorState
{
    public override int EnterState()
    {
        return StateID;
    }

    public override SpectatorState Transition()
    {
        if (NumGenerator.Next(0, 2) == 0)
        {
            return new IdleState();
        }
        else
        {
            return new LookAroundState();
        }
    }
}
