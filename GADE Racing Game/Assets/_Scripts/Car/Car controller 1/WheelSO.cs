using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelSO", menuName = "Scriptable Object/Car/Wheel")]
public class WheelSO : ScriptableObject
{
    //public bool IsFront; // To differentiate between front and back wheels
    public Transform WheelTransform;
    public float Radius;  // Used for spherecasting or raycasting
    [HideInInspector] public RaycastHit hitInfo;
    [HideInInspector] public bool IsWheelGrounded;
    [Space]
    [Header("Suspension")]
    public float SpringStrength;
    public float SpringDamper;
    public float SpringRestDistance;
    [Space]
    public float WheelMass;
    public float TireGripStrength;
}
