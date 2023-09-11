using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInput : InputController
{
    public override float GetAccelerationInput()
    {
        return 0;
    }
    public override float GetSteeringInput()
    {
        return 0;
    }
}
