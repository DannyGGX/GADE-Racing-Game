using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputController
{
    public virtual float GetAccelerationInput()
    {
        return 0;
    }
    public virtual float GetSteeringInput()
    {
        return 0;
    }
}
