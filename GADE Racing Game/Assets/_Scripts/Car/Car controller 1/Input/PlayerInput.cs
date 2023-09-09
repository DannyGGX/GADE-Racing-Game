using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : InputController
{
    private void Update()
    {
        turnInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Fire1"))
        {
            this.Log(turnInput);
        }
    }

}
