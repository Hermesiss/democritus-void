using System;
using UnityEngine;

public class ShipController {


    public float RotationSpeed { get; private set; }
    public float MovementSpeed { get; private set; }
    public float MovementDamping { get; private set; }
    public float MaximumSpeed { get; private set; }
    public float BrakingForce { get; private set; }

    public bool IsBraking;
    public Vector3 Direction;
    public Vector2 Velocity { get; private set; }

    private Transform _t;


    private float SmoothingAngle => RotationSpeed / 8;

    public ShipController(float rotationSpeed,
        float movementSpeed,
        float movementDamping,
        Transform t,
        float maximumSpeed,
        float brakingForce) {
        RotationSpeed = rotationSpeed;
        MovementSpeed = movementSpeed;
        MovementDamping = movementDamping;
        _t = t;
        MaximumSpeed = maximumSpeed;
        BrakingForce = brakingForce;
    }

    public void RotateAt(Vector3 target) {
        Vector2 projection = target - _t.position;

        var angle = Vector3.SignedAngle(_t.up, projection, _t.forward);

        angle = Mathf.Abs(angle) > SmoothingAngle ? Mathf.Sign(angle) : angle / SmoothingAngle;

        _t.Rotate(_t.forward, angle * Time.deltaTime * RotationSpeed);
        Debug.DrawRay(_t.position, _t.forward);
    }

    public void MoveToDirection() {
        float momentum;
        if (Direction.sqrMagnitude > float.Epsilon) {
            momentum = MovementSpeed * (1 - MovementDamping) * 1;
        }
        else {
            var inertia = IsBraking ? BrakingForce : 1;
            Direction = -Velocity;
            momentum = Mathf.Clamp(MovementSpeed * MovementDamping * inertia, 0,
                MovementDamping * Velocity.magnitude / Time.deltaTime);
        }

        Velocity += (Vector2) Direction.normalized * momentum * Time.deltaTime;

        if (Velocity.sqrMagnitude > MaximumSpeed * MaximumSpeed) {
            Velocity = Velocity.normalized * MaximumSpeed;
        }

        _t.Translate(Velocity * Time.deltaTime, Space.World);

        Direction = Vector3.zero;
        IsBraking = false;
    }
}