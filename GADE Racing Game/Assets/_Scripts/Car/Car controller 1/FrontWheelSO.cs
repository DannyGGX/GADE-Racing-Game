using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrontWheelSO", menuName = "Scriptable Object/Car/Front Wheel")]
public class FrontWheelSO : WheelSO
{
    [Range(20, 60)] public float MaxSteeringAngle;
}
