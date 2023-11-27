using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController
{
    public bool InputEnabled { get; set; } = true;
    public virtual float GetAccelerationInput()
    {
        if (InputEnabled)
            return Input.GetAxis("Vertical");
        else
            return 0;
    }
    public virtual float GetSteeringInput()
    {
        if (InputEnabled)
            return Input.GetAxis("Horizontal");
        else
            return 0;
    }
    public virtual bool GetHandBrakeInput()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }
}
