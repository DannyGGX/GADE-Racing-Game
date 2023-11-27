using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class CarController3 : MonoBehaviour
{
    [SerializeField, Tooltip("y-axis: Acceleration/ Torque | x-axis: current speed")] 
    private AnimationCurve accelerationCurve;
    
    [SerializeField, Tooltip("In degrees")] private float maxSteeringAngle = 25;
    [SerializeField] private float frontWheelsBrakeForce = 10000;
    [SerializeField] private float backWheelsBrakeForce = 10000;
    [Space]
    [SerializeField] private WheelCollider frontLeftWheel, frontRightWheel;
    [SerializeField] private WheelCollider backLeftWheel, backRightWheel;
    [SerializeField] private bool frontWheelDrive = true;
    [SerializeField] private bool backWheelDrive = false;
    [Space] 
    [SerializeField] private Transform frontLeftMesh, frontRightMesh;
    [SerializeField] private Transform backLeftMesh, backRightMesh;
    
    private List<WheelCollider> driveWheels;
    
    [HideInInspector] public InputController InputController;
    private float currentAcceleration;
    private float currentSteeringAngle;
    private float currentBrakeForceFront; // brake force for the front wheels
    private float currentBrakeForceBack; // brake force for the back wheels
    private float currentSpeed;
    private Rigidbody rigidBody;

    private void OnEnable()
    {
        InputController = new InputController();
        rigidBody = GetComponent<Rigidbody>();

        driveWheels = new List<WheelCollider>(4);
        if (frontWheelDrive)
        {
            driveWheels.Add(frontLeftWheel);
            driveWheels.Add(frontRightWheel);
        }
        if (backWheelDrive)
        {
            driveWheels.Add(backLeftWheel);
            driveWheels.Add(backRightWheel);
        }
    }

    private void FixedUpdate()
    {
        HandleAcceleration();
        HandleBraking();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleAcceleration()
    {
        currentSpeed = rigidBody.velocity.magnitude;
        currentAcceleration = InputController.GetAccelerationInput() * accelerationCurve.Evaluate(currentSpeed);
        for (int i = 0; i < driveWheels.Count; i++)
        {
            driveWheels[i].motorTorque = currentAcceleration;
        }
    }

    private void HandleSteering()
    {
        currentSteeringAngle = maxSteeringAngle * InputController.GetSteeringInput();
        frontLeftWheel.steerAngle = currentSteeringAngle;
        frontRightWheel.steerAngle = currentSteeringAngle;
    }

    private void HandleBraking()
    {
        currentBrakeForceFront = InputController.GetHandBrakeInput() ? frontWheelsBrakeForce : 0;
        currentBrakeForceBack = InputController.GetHandBrakeInput() ? backWheelsBrakeForce : 0;

        frontLeftWheel.brakeTorque = currentBrakeForceFront;
        frontRightWheel.brakeTorque = currentBrakeForceFront;
        backLeftWheel.brakeTorque = currentBrakeForceBack;
        backRightWheel.brakeTorque = currentBrakeForceBack;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheel, frontLeftMesh);
        UpdateSingleWheel(frontRightWheel, frontRightMesh);
        UpdateSingleWheel(backLeftWheel, backLeftMesh);
        UpdateSingleWheel(backRightWheel, backRightMesh);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out var position, out var rotation);
        rotation.y += 90;
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;
    }
}

