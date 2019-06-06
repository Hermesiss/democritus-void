using System;
using UnityEngine;

namespace Inventory {
    [CreateAssetMenu(fileName = "New ShipWeapon", menuName = "Game/ShipWeapon", order = 51)]
    public class ShipWeapon : ShipItem {
        [Serializable]
        public enum Variant {
            None,
            Laser,
            Kinetic,
            Plasma
        }
        
        public float Angle;
        public float Range;
        public float ProjectileSpeed;
        public float FireRate;
        public Variant WeaponVariant;

        public ShipWeapon(ItemParams itemParams, ShipItemParams shipItemParams, 
            float angle, 
            float range,
            float projectileSpeed, 
            float fireRate, 
            Variant weaponVariant) : base(itemParams, shipItemParams) {
            Angle = angle;
            Range = range;
            ProjectileSpeed = projectileSpeed;
            FireRate = fireRate;
            WeaponVariant = weaponVariant;
        }
        public override ShipItemType ItemType => ShipItemType.Weapon;
    }
}