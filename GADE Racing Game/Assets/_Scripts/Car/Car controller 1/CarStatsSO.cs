using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "CarSO", menuName = "Scriptable Object/Car/Car Stats")]
public class CarStatsSO : ScriptableObject
{
    public float TopSpeed = 100;
    public AnimationCurve PowerCurve;
}
