using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackWheel
{
    public Transform WheelTransform;
    public WheelSO Stats;
    [HideInInspector] public RaycastHit hitInfo;
    [HideInInspector] public bool IsWheelGrounded;
}
