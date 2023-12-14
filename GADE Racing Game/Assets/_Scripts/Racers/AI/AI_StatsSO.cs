using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI_StatsSO", menuName = "Scriptable Object/AI/Racer Stats")]
public class AI_StatsSO : ScriptableObject
{
    public GameObject CarModel;
    [Space]
    public float Speed;
    public float AngularSpeed;
    public float Acceleration;
    public bool AutoBraking = true;
}
