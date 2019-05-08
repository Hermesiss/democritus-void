using System;
using UnityEngine;

public class ShipController {

    public void SetRotationSpeed(float value) => _rotationSpeed = value < 0 ?  
        throw new ArgumentOutOfRangeException(nameof(value), "Must be greater or equal to zero") : 
        value;
    
    private float _rotationSpeed;
    private float SmoothingAngle => _rotationSpeed / 8;
    public ShipController(float rotationSpeed) {
        _rotationSpeed = rotationSpeed;
    }

    public void RotateAt(Vector3 target, Transform t) {
        Vector2 projection = target - t.position;

        var angle = Vector3.SignedAngle(t.up, projection, t.forward);

        angle = Mathf.Abs(angle) > SmoothingAngle ? Mathf.Sign(angle) : angle / SmoothingAngle;

        t.Rotate(t.forward, angle * Time.deltaTime * _rotationSpeed);
        Debug.DrawRay(t.position, t.forward);
    }
}