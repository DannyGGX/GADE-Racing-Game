using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetRotator : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float rotationSpeed = 10;

    private void FixedUpdate()
    {
        if (rigidBody.velocity.magnitude > 0.1f)
        {
            RotateTowardsMovementDirection();
        }
    }

    private void RotateTowardsMovementDirection()
    {
        Vector3 movementDirection = rigidBody.velocity.normalized;
        Quaternion toRotation = Quaternion.LookRotation(movementDirection);
        
        // Smoothly rotate the object towards the movement direction
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}
