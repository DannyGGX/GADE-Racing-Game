using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : InputController
{
    public float GetAccelerationInput()
    {
        return Input.GetAxis("Vertical");
    }
    public float GetSteeringInput()
    {
        return Input.GetAxis("Horizontal");
    }

}
