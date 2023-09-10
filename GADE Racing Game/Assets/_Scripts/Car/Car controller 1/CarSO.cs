using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "CarSO", menuName = "Scriptable Object/Car/Car")]
public class CarSO : ScriptableObject
{
    public float TopSpeed = 100;
    public AnimationCurve PowerCurve;
    [Space]
    public FrontWheelSO[] FrontWheels = new FrontWheelSO[2];
    public WheelSO[] BackWheels = new WheelSO[2];

}
