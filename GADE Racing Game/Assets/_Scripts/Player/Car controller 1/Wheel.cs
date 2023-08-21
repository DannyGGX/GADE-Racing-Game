using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wheel
{
    public bool IsFront; // To differentiate between front and back wheels
    public Transform WheelTransform;
    public float Radius;  // Used for spherecasting
    [Space]
    [Header("Suspension")]
    public float SpringStrength;
    public float SpringDamper;
    public float SpringRestDistance;

}
