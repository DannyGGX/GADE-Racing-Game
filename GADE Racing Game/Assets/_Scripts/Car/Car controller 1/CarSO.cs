using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarSO : ScriptableObject
{
    public float TopSpeed = 100;
    public AnimationCurve PowerCurve;
    [Space]
    public WheelSO[] FrontWheels = new WheelSO[2];
    public WheelSO[] BackWheels = new WheelSO[2];

}
