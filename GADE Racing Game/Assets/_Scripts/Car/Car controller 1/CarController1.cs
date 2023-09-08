using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private LayerMask ground; // for wheel collisions
    [SerializeField] private WheelSO[] wheels = new WheelSO[4];
    [SerializeField] private InputController inputController;

    private void Awake()
    {
        
    }

    private void Update() // Detect inputs in here
    {
        
    }

    private void FixedUpdate() // Calculate physics here
    {
        foreach(var wheel in wheels)
        {
            wheel.IsWheelGrounded = CheckWheelToGroundCollision(wheel.WheelTransform, wheel.Radius);
        }
        foreach(var wheel in wheels)
        {
            //Calculate Physics
            if (wheel.IsWheelGrounded)
            {

                if(wheel is FrontWheelSO)
                {

                }
            }
        }
    }

    private bool CheckWheelToGroundCollision(Transform wheelTransform, float wheelRadius)
    {
        if (Physics.SphereCast(wheelTransform.position, wheelRadius, Vector3.down, out RaycastHit hitinfo, wheelRadius, ground))
            return true;
        else
            return false;
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

    private void ApplySteeringForce(Transform tireTransform, float tireGrip, float tireMass)
    {
        Vector3 steeringDir = tireTransform.right;
        Vector3 wheelWorldVelocity = rigidBody.GetPointVelocity(tireTransform.position);

        float steeringVel = Vector3.Dot(steeringDir, wheelWorldVelocity);
        float desiredVelChange = -steeringVel * tireGrip;
        float desiredAcceleration = desiredVelChange / Time.fixedDeltaTime;
        rigidBody.AddForceAtPosition(steeringDir * tireMass * desiredAcceleration, tireTransform.position);
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

    private void TurnFrontWheels(float turnMagnitude)
    {

    }
}
