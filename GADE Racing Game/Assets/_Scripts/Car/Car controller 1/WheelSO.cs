using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelSO", menuName = "Scriptable Object/Car/Back Wheel")]
public class WheelSO : ScriptableObject
{
    public float Radius;  // Used for spherecasting or raycasting
    [Space]
    [Header("Suspension")]
    public float SpringStrength;
    public float SpringDamper;
    public float SpringRestDistance;
    [Space]
    public float WheelMass;
    public float TireGripStrength;
}
