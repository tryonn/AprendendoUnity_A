using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMotor : MonoBehaviour {

    [HideInInspector]
    public Vector3 movementDirection;

    private Rigidbody _rigidbody;

    public float walkingSpeed = 5f;
    public float walkingSnapyness = 50f;
    public float turningSmoothing = .3f;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = movementDirection * walkingSpeed;
        Vector3 deltaVelocity = targetVelocity - _rigidbody.velocity;

        if (_rigidbody.useGravity)
            deltaVelocity.y = 0;

        _rigidbody.AddForce(deltaVelocity * walkingSnapyness, ForceMode.Acceleration);

        Vector3 faceDir = movementDirection;

        if (faceDir == Vector3.zero)
        {
            _rigidbody.angularVelocity = Vector3.zero;
        } else
        {
            float rotationAngle = AngleAroundAxis(transform.forward, faceDir, Vector3.up);
            _rigidbody.angularVelocity = (Vector3.up * rotationAngle * turningSmoothing);
        }
    }

    private float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
    {
        dirA = dirA - Vector3.Project(dirA, axis);
        dirB = dirB - Vector3.Project(dirB, axis);
        
        //busca positividade entre o angulo A e B 
        float angle = Vector3.Angle(dirA, dirB);

        return angle * (Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
    }
}
