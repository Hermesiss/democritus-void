using System;
using UnityEngine;

public class ShipController {

    public void SetRotationSpeed(float value) => _rotationSpeed = value < 0
        ? throw new ArgumentOutOfRangeException(nameof(value), "Must be greater or equal to zero")
        : _rotationSpeed = value;

    public void SetMovementSpeed(float value) => _movementSpeed = value < 0
        ? throw new ArgumentOutOfRangeException(nameof(value), "Must be greater or equal to zero")
        : _movementSpeed = value;

    public void SetMovementDamping(float value) => _movementDamping = value < 0
        ? throw new ArgumentOutOfRangeException(nameof(value), "Must be greater or equal to zero")
        : _movementDamping = value;

    public void SetMaximumSpeed(float value) => _maximumSpeed = value < 0
        ? throw new ArgumentOutOfRangeException(nameof(value), "Must be greater or equal to zero")
        : _maximumSpeed = value;

    public void SetDirection(Vector3 direction) => _direction = direction;
    public void SetInertiaMultiplier(float multiplier) => _inertiaMultiplier = multiplier;

    private float _rotationSpeed;
    private float _movementSpeed;
    private float _movementDamping;
    private float _maximumSpeed;
    private Transform _t;

    private Vector2 _velocity;

    private Vector3 _direction;
    private float _inertiaMultiplier;

    private float SmoothingAngle => _rotationSpeed / 8;

    public ShipController(float rotationSpeed,
        float movementSpeed,
        float movementDamping,
        Transform t,
        float maximumSpeed) {
        _rotationSpeed = rotationSpeed;
        _movementSpeed = movementSpeed;
        _movementDamping = movementDamping;
        _t = t;
        _maximumSpeed = maximumSpeed;
    }

    public void RotateAt(Vector3 target) {
        Vector2 projection = target - _t.position;

        var angle = Vector3.SignedAngle(_t.up, projection, _t.forward);

        angle = Mathf.Abs(angle) > SmoothingAngle ? Mathf.Sign(angle) : angle / SmoothingAngle;

        _t.Rotate(_t.forward, angle * Time.deltaTime * _rotationSpeed);
        Debug.DrawRay(_t.position, _t.forward);
    }

    public void MoveToDirection() {
        float momentum;
        if (_direction.sqrMagnitude > float.Epsilon) {
            momentum = _movementSpeed * (1 - _movementDamping) * _inertiaMultiplier;
        }
        else {
            _direction = -_velocity;
            momentum = Mathf.Clamp(_movementSpeed * _movementDamping * _inertiaMultiplier, 0, _movementDamping*_velocity.magnitude/Time.deltaTime);
        }

        _velocity += (Vector2) _direction.normalized * momentum * Time.deltaTime;

        if (_velocity.sqrMagnitude > _maximumSpeed * _maximumSpeed) {
            _velocity = _velocity.normalized * _maximumSpeed;
        }

        _t.Translate(_velocity * Time.deltaTime, Space.World);

        _direction = Vector3.zero;
        _inertiaMultiplier = 1;
    }
}