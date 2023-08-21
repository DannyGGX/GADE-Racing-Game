using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private LayerMask ground; // for wheel collisions
    [SerializeField] private Wheel[] wheels = new Wheel[4];

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

        Vector3 wheelWorldVelocity = rigidbody.GetPointVelocity(wheelTransform.position);

        float offset = springRestDistance - hit.distance;

        float velocity = Vector3.Dot(springDir, wheelWorldVelocity);
        float force = (offset * springStrength) - (velocity * springDamper);
        rigidbody.AddForceAtPosition(springDir * force, wheelTransform.position);
    }
}
