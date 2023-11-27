using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInput : InputController
{
    public override float GetAccelerationInput()
    {
        return Input.GetAxis("Vertical");
    }
    public override float GetSteeringInput()
    {
        return Input.GetAxis("Horizontal");
    }

}
