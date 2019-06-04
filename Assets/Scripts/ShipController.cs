using System;
using Inventory;
using UnityEngine;

public struct ShipParameters {
    public readonly int Weapons;
    public readonly int RearEngines;
    public readonly int SideEngines;
    public readonly int Shields;
    public readonly int Generators;

    public ShipParameters(int weapons, int rearEngines, int sideEngines, int shields, int generators) {
        Weapons = weapons;
        RearEngines = rearEngines;
        SideEngines = sideEngines;
        Shields = shields;
        Generators = generators;
    }
}

public struct ShipAttributes {
    public readonly float RotationSpeed;
    public readonly float MovementSpeed;
    public readonly float MovementDamping;
    public readonly float MaximumSpeed;
    public readonly float BrakingForce;
    public float SmoothingAngle => RotationSpeed / 8;
    public ShipAttributes(float rotationSpeed, float movementSpeed, float movementDamping, float maximumSpeed, float brakingForce) {
        RotationSpeed = rotationSpeed;
        MovementSpeed = movementSpeed;
        MovementDamping = movementDamping;
        MaximumSpeed = maximumSpeed;
        BrakingForce = brakingForce;
    }
}

[Serializable]
public class ShipController {
    
    public ShipAttributes Attributes { get; private set; }
    public bool IsBraking;
    public Vector3 Direction;
    public Vector2 Velocity { get; private set; }

    private Transform _t;

    public readonly ShipParameters Parameters;
    public readonly IItemCollection<ShipWeapon> Weapons;
    public readonly IItemCollection<ShipEngine> RearEngines;
    public readonly IItemCollection<ShipEngine> SideEngines;
    public readonly IItemCollection<ShipShield> Shields;
    public readonly IItemCollection<ShipGenerator> Generators;

    public ShipController(ShipParameters parameters, Transform t) {
        Parameters = parameters;
        _t = t;
        Weapons = new ItemCollection<ShipWeapon>(5, parameters.Weapons);
        RearEngines = new ItemCollection<ShipEngine>(3, parameters.RearEngines);
        SideEngines = new ItemCollection<ShipEngine>(3, parameters.SideEngines);
        Shields = new ItemCollection<ShipShield>(3, parameters.Shields);
        Generators = new ItemCollection<ShipGenerator>(3, parameters.Generators);
    }

    private void ResizeItemsArray<T>(int newSize, IItemCollection<T> items)
        where T : ShipItem {

        var extra = items.Resize(newSize);
        AddToInventory(extra);
    }

    private void AddToInventory<T>(T[] extra) where T : ShipItem {
        throw new NotImplementedException();
    }

    public void RotateAt(Vector3 target) {
        var position = _t.position;
        var forward = _t.forward;
        Vector2 projection = target - position;
        
        var angle = Vector3.SignedAngle(_t.up, projection, forward);

        angle = Mathf.Abs(angle) > Attributes.SmoothingAngle ? Mathf.Sign(angle) : angle / Attributes.SmoothingAngle;

        _t.Rotate(forward, angle * Time.deltaTime * Attributes.RotationSpeed);
        Debug.DrawRay(position, _t.forward);
    }

    public void MoveToDirection() {
        float momentum;
        if (Direction.sqrMagnitude > float.Epsilon) {
            momentum = Attributes.MovementSpeed * (1 - Attributes.MovementDamping) * 1;
        }
        else {
            var inertia = IsBraking ? Attributes.BrakingForce : 1;
            Direction = -Velocity;
            momentum = Mathf.Clamp(Attributes.MovementSpeed * Attributes.MovementDamping * inertia, 0,
                Attributes.MovementDamping * Velocity.magnitude / Time.deltaTime);
        }

        Velocity += momentum * Time.deltaTime * (Vector2) Direction.normalized;

        if (Velocity.sqrMagnitude > Attributes.MaximumSpeed * Attributes.MaximumSpeed) {
            Velocity = Velocity.normalized * Attributes.MaximumSpeed;
        }

        _t.Translate(Velocity * Time.deltaTime, Space.World);

        Direction = Vector3.zero;
        IsBraking = false;
    }
}