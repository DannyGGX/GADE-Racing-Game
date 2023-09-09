using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float forwardInput;
    public float turnInput;
    [Space]
    [SerializeField] private float maxWheelTurn = 45;

    public Vector3 turnInputToWheelTurn(float turnInput)
    {
        float turnDegrees = Mathf.Lerp(-maxWheelTurn, maxWheelTurn, turnInput);
        float turnRadians = Mathf.Deg2Rad * turnDegrees;
        Vector3 turn = new Vector3(0, turnRadians);
        return turn;
    }
}
