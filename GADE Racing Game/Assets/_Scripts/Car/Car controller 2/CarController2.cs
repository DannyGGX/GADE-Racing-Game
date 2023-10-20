using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : Racer
{
    public InputController InputController;
    [Space]
    [SerializeField] WheelCollider frontLeftWheel;
    [SerializeField] WheelCollider frontRightWheel;
    [SerializeField] WheelCollider backLeftWheel;
    [SerializeField] WheelCollider backRightWheel;
    [Space] 
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform backLeftTransform;
    [SerializeField] private Transform backRightTransform;
    [Space]
    [SerializeField] private float maxAcceleration = 500;
    [SerializeField] private float maxBrakeForce = 300;
    [SerializeField] private float maxTurnAngle = 15;

    private float currentAcceleration;
    private float currentBrakeForce;
    private float currentTurnAngle;

    private void OnEnable() // Called before Awake in RaceManager
    {
        InputController = new PlayerInput();
    }

    private void FixedUpdate()
    {
        currentAcceleration = maxAcceleration * InputController.GetAccelerationInput();


        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentBrakeForce = maxBrakeForce;

        }
        else
        {
            currentBrakeForce = 0;
        }
        frontLeftWheel.motorTorque = currentAcceleration;
        frontRightWheel.motorTorque = currentAcceleration;

        frontLeftWheel.brakeTorque = currentBrakeForce;
        frontRightWheel.brakeTorque = currentBrakeForce;
        backLeftWheel.brakeTorque = currentBrakeForce;
        backRightWheel.brakeTorque = currentBrakeForce;

        ApplySteering();
    }

    private void ApplySteering()
    {
        currentTurnAngle = maxTurnAngle * InputController.GetSteeringInput();
        frontLeftWheel.steerAngle = currentTurnAngle;
        frontRightWheel.steerAngle = currentTurnAngle;
        
        ApplySteeringAngle(InputController.GetSteeringInput(), maxTurnAngle, frontLeftTransform);
        ApplySteeringAngle(InputController.GetSteeringInput(), maxTurnAngle, frontRightTransform);
    }
    
    private void ApplySteeringAngle(float steerInput, float maxSteeringAngle, Transform wheelTransform)
    {
        float steerAngle = maxSteeringAngle * steerInput;
        wheelTransform.localRotation = Quaternion.Euler(0, steerAngle, 90);
    }
}
