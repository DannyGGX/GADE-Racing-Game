using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FrontWheel
{
    public Transform WheelTransform;
    public FrontWheelSO Stats;
    [HideInInspector] public RaycastHit hitInfo;
    [HideInInspector] public bool IsWheelGrounded;
}
