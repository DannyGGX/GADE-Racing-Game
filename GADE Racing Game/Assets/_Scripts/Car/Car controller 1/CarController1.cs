using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private LayerMask ground; // for wheel collisions
    [SerializeField] private CarSO car;
    //[SerializeField] private InputController inputController;
    [Space]
    [SerializeField] private Transform[] frontWheels = new Transform[2];
    [SerializeField] private Transform[] backWheels = new Transform[2];

    private float accelerationInput;
    private float steeringInput;

    private void Awake()
    {
        
    }

    private void Update()
    {
        DetectInputs();
    }

    private void FixedUpdate() // Calculate physics here
    {
        foreach (var frontWheel in car.FrontWheels)
        {
            frontWheel.IsWheelGrounded = CheckWheelToGroundCollision(frontWheel.WheelTransform, frontWheel.Radius, out frontWheel.hitInfo);
        }
        foreach(var backWheel in car.BackWheels)
        {
            backWheel.IsWheelGrounded = CheckWheelToGroundCollision(backWheel.WheelTransform, backWheel.Radius, out backWheel.hitInfo);
        }

        foreach(var frontWheel in car.FrontWheels)
        {
            if (frontWheel.IsWheelGrounded)
            {
                //Calculate Physics
                ApplySuspensionForce(frontWheel.WheelTransform, frontWheel.SpringStrength, frontWheel.SpringDamper, frontWheel.SpringRestDistance, frontWheel.hitInfo);
                ApplySteeringAngle(steeringInput, frontWheel.MaxSteeringAngle, frontWheel.WheelTransform);
                ApplySteeringForce(frontWheel.WheelTransform, frontWheel.TireGripStrength, frontWheel.WheelMass);
                ApplyAccelerationForce(frontWheel.WheelTransform, transform, accelerationInput, car.TopSpeed, car.PowerCurve);
            }
        }
        foreach (var backWheel in car.BackWheels)
        {
            if (backWheel.IsWheelGrounded)
            {
                //Calculate Physics
                ApplySuspensionForce(backWheel.WheelTransform, backWheel.SpringStrength, backWheel.SpringDamper, backWheel.SpringRestDistance, backWheel.hitInfo);
            }
        }
    }

    private void DetectInputs()
    {
        accelerationInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
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
        float desiredAcceleration = desiredVelChange / Time.fixedDeltaTime;
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
