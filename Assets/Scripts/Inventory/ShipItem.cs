namespace Inventory {
    public enum ShipItemType {
        None,
        RearEngine,
        SideEngine,
        Weapon,
        Shield,
        Generator
    }

    public struct EnergyConsumption {
        public enum Mode {
            None,
            PerSecond,
            PerUsage
        }

        public readonly Mode ConsumptionMode;
        public readonly float ConsumptionValue;

        public override string ToString() => $"{ConsumptionMode.ToString()}: {ConsumptionValue}";
        public EnergyConsumption(Mode consumptionMode = Mode.None, float consumptionValue = 0) {
            ConsumptionMode = consumptionMode;
            ConsumptionValue = consumptionValue;
        }
    }

    public struct ShipItemParams {
        public readonly float RotationSpeed;
        public readonly float MovementSpeed;
        public readonly float MovementDamping;
        public readonly float MaximumSpeed;
        public readonly float BrakingForce;
        public readonly EnergyConsumption EnergyConsumption;
        
        public ShipItemParams(float rotationSpeed = 0,
            float movementSpeed = 0,
            float movementDamping = 0,
            float maximumSpeed = 0,
            float brakingForce = 0,
            EnergyConsumption energyConsumption = default
            ) {
            RotationSpeed = rotationSpeed;
            MovementSpeed = movementSpeed;
            MovementDamping = movementDamping;
            MaximumSpeed = maximumSpeed;
            BrakingForce = brakingForce;
            EnergyConsumption = energyConsumption;
        }
    }

    public abstract class ShipItem : ItemBase<ShipItemType> {

        public override string ToString() => $"{RotationSpeed}, {MovementSpeed}, {MovementDamping}, {MaximumSpeed}, {BrakingForce}, {EnergyConsumption}";

        public readonly float RotationSpeed;
        public readonly float MovementSpeed;
        public readonly float MovementDamping;
        public readonly float MaximumSpeed;
        public readonly float BrakingForce;
        public readonly EnergyConsumption EnergyConsumption;


        protected ShipItem(ItemParams itemParams, ShipItemParams shipItemParams) : base(itemParams) {
            RotationSpeed = shipItemParams.RotationSpeed;
            MovementSpeed = shipItemParams.MovementSpeed;
            MovementDamping = shipItemParams.MovementDamping;
            MaximumSpeed = shipItemParams.MaximumSpeed;
            BrakingForce = shipItemParams.BrakingForce;
            EnergyConsumption = shipItemParams.EnergyConsumption;
        }
    }

    public class ShipWeapon : ShipItem {
        public enum Variant {
            None,
            Laser,
            Kinetic,
            Plasma
        }
        
        public readonly float Angle;
        public readonly float Range;
        public readonly float ProjectileSpeed;
        public readonly float FireRate;
        public readonly Variant WeaponVariant;

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

    public class ShipRearEngine : ShipItem {
        public ShipRearEngine(ItemParams itemParams, ShipItemParams shipItemParams) : base(itemParams, shipItemParams) {
            
        }
        public override ShipItemType ItemType => ShipItemType.RearEngine;
    }
    
    public class ShipSideEngine : ShipItem {
        public ShipSideEngine(ItemParams itemParams, ShipItemParams shipItemParams) : base(itemParams, shipItemParams) {
            
        }
        public override ShipItemType ItemType => ShipItemType.SideEngine;
    }

    public class ShipShield : ShipItem {
        public readonly float DamageCapacity;
        public readonly float RecoverySpeed;
        public ShipShield(ItemParams itemParams, ShipItemParams shipItemParams, 
            float damageCapacity, float recoverySpeed) : base(itemParams, shipItemParams) {
            DamageCapacity = damageCapacity;
            RecoverySpeed = recoverySpeed;
        }
        public override ShipItemType ItemType => ShipItemType.Shield;
    }

    public class ShipGenerator : ShipItem {
        public readonly float GenerationSpeed;
        public readonly float EnergyCapacity;
        public ShipGenerator(ItemParams itemParams, ShipItemParams shipItemParams, 
            float generationSpeed, float energyCapacity) : base(itemParams, shipItemParams) {
            GenerationSpeed = generationSpeed;
            EnergyCapacity = energyCapacity;
        }
        public override ShipItemType ItemType => ShipItemType.Generator;
    }
}