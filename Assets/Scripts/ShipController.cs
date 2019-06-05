using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using UnityEngine;

[Serializable]
public class ShipController {
    
    public ShipAttributes Attributes { get; private set; }
    public bool IsBraking;
    public Vector3 Direction;
    public Vector2 Velocity { get; private set; }

    private Transform _t;

    public readonly ShipParameters Parameters;
    public readonly IItemCollection<ShipWeapon> Weapons;
    public readonly IItemCollection<ShipRearEngine> RearEngines;
    public readonly IItemCollection<ShipSideEngine> SideEngines;
    public readonly IItemCollection<ShipShield> Shields;
    public readonly IItemCollection<ShipGenerator> Generators;
    public readonly Dictionary<ShipItemType, IItemCollection<ShipItem>> Items;
    

    public ShipController(ShipParameters parameters, Transform t) {
        Parameters = parameters;
        _t = t;
        Weapons = new ItemCollection<ShipWeapon>(5, parameters.Weapons);
        RearEngines = new ItemCollection<ShipRearEngine>(3, parameters.RearEngines);
        SideEngines = new ItemCollection<ShipSideEngine>(3, parameters.SideEngines);
        Shields = new ItemCollection<ShipShield>(3, parameters.Shields);
        Generators = new ItemCollection<ShipGenerator>(3, parameters.Generators);

        Items = new Dictionary<ShipItemType, IItemCollection<ShipItem>> {
            {ShipItemType.Weapon, Weapons},
            {ShipItemType.RearEngine, RearEngines},
            {ShipItemType.SideEngine, SideEngines},
            {ShipItemType.Shield, Shields},
            {ShipItemType.Generator, Generators}
        };
    }

    private void RecalculateAttributes() {
        var rotationSpeed = Items.Sum(x => x.Value.Where(z => z != null).Sum(z => z.RotationSpeed));
        var movementSpeed = Items.Sum(x => x.Value.Where(z => z != null).Sum(z => z.MovementSpeed));
        var movementDamping = Items.Sum(x => x.Value.Where(z => z != null).Sum(z => z.MovementDamping));
        var brakingForce = Items.Sum(x => x.Value.Where(z => z != null).Sum(z => z.BrakingForce));
        var maximumSpeed = Items.Sum(x => x.Value.Where(z => z != null).Sum(z => z.MaximumSpeed));
        Attributes = new ShipAttributes(rotationSpeed, movementSpeed, movementDamping, maximumSpeed, brakingForce);
    }

    public ShipItem PlaceItem<T>(ShipItemType itemType, T item, int index) where T: ShipItem {
        var oldItem = Items[itemType].Add(item, index);
        RecalculateAttributes();
        return oldItem;
    }

    public ShipItem RemoveItem(ShipItemType itemType, int index) {
        var removedItem = Items[itemType].Remove(index);
        RecalculateAttributes();
        return removedItem;
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