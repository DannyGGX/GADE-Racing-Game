using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private LayerMask ground; // for wheel collisions
    [SerializeField] private Car car;
    [HideInInspector] public InputController InputController; // public so that it can be accessed by RaceManager to disable inputs

    private float accelerationInput;
    private float steeringInput;

    private void OnEnable()
    {
        InputController = new PlayerInput();
    }

    private void Update()
    {
        DetectInputs();
    }

    private void FixedUpdate() // Calculate physics here
    {
        foreach (var frontWheel in car.FrontWheels)
        {
            frontWheel.IsWheelGrounded = RaycastGroundDetection(frontWheel.WheelTransform, frontWheel.Stats.Radius, out frontWheel.hitInfo);
        }
        foreach(var backWheel in car.BackWheels)
        {
            backWheel.IsWheelGrounded = RaycastGroundDetection(backWheel.WheelTransform, backWheel.Stats.Radius, out backWheel.hitInfo);
        }

        foreach(var frontWheel in car.FrontWheels)
        {
            ApplySteeringAngle(steeringInput, frontWheel.Stats.MaxSteeringAngle, frontWheel.WheelTransform);
            if (frontWheel.IsWheelGrounded)
            {
                //Calculate Physics
                ApplySuspensionForce(frontWheel.WheelTransform, frontWheel.Stats.SpringStrength, frontWheel.Stats.SpringDamper, frontWheel.Stats.SpringRestDistance, frontWheel.hitInfo);
                ApplySteeringForce(frontWheel.WheelTransform, frontWheel.Stats.TireGripStrength, frontWheel.Stats.WheelMass);
                ApplyAccelerationForce(frontWheel.WheelTransform, transform, accelerationInput, car.CarStats.TopSpeed, car.CarStats.PowerCurve);
            }
        }
        foreach (var backWheel in car.BackWheels)
        {
            if (backWheel.IsWheelGrounded)
            {
                //Calculate Physics
                ApplySuspensionForce(backWheel.WheelTransform, backWheel.Stats.SpringStrength, backWheel.Stats.SpringDamper, backWheel.Stats.SpringRestDistance, backWheel.hitInfo);
            }
        }
    }

    private void DetectInputs()
    {
        accelerationInput = InputController.GetAccelerationInput();
        steeringInput = InputController.GetSteeringInput();
    }

    private bool CheckWheelToGroundCollision(Transform wheelTransform, float wheelRadius, out RaycastHit hitInfo)
    {
        if (Physics.SphereCast(wheelTransform.position, wheelRadius, Vector3.down, out hitInfo, wheelRadius, ground))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool RaycastGroundDetection(Transform wheelTransform, float wheelRadius, out RaycastHit hitInfo)
    {
        if (Physics.Raycast(wheelTransform.position, Vector3.down, out hitInfo, wheelRadius, ground))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ApplySuspensionForce(Transform wheelTransform, float springStrength, float springDamper, float springRestDistance, RaycastHit hit)
    {
        Vector3 springDir = wheelTransform.up;

        Vector3 wheelWorldVelocity = rigidBody.GetPointVelocity(wheelTransform.position);

        float offset = springRestDistance - hit.distance;

        float velocity = Vector3.Dot(springDir, wheelWorldVelocity);
        float force = (offset * springStrength) - (velocity * springDamper);
        rigidBody.AddForceAtPosition(springDir * force, wheelTransform.position);
    }

    private void ApplySteeringForce(Transform tireTransform, float tireGrip, float wheelMass)
    {
        Vector3 steeringDir = tireTransform.right;
        Vector3 wheelWorldVelocity = rigidBody.GetPointVelocity(tireTransform.position);

        float steeringVel = Vector3.Dot(steeringDir, wheelWorldVelocity);
        float desiredVelChange = -steeringVel * tireGrip;
        float desiredAcceleration = desiredVelChange * Time.deltaTime;
        rigidBody.AddForceAtPosition(steeringDir * wheelMass * desiredAcceleration, tireTransform.position);
    }

    private void ApplyAccelerationForce(Transform tireTransform, Transform carTransform, float accelerationInput, float carTopSpeed, AnimationCurve powerCurve)
    {
        Vector3 accelerationDir = tireTransform.forward;

        if(accelerationInput > 0.0f)
        {
            float carSpeed = Vector3.Dot(carTransform.forward, rigidBody.velocity);
            float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

            float availableTorque = powerCurve.Evaluate(normalizedSpeed) * accelerationInput;

            rigidBody.AddForceAtPosition(accelerationDir * availableTorque, tireTransform.position);
        }
    }

    private void ApplySteeringAngle(float steerInput, float maxSteeringAngle, Transform wheelTransform)
    {
        float steerAngle = maxSteeringAngle * steerInput;
        wheelTransform.localRotation = Quaternion.Euler(0, steerAngle, 0);
    }

}
