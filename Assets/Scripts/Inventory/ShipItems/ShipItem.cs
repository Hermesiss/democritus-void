using System;

namespace Inventory.ShipItems {
    
    public abstract class ShipItem : ItemBase<ShipItemType> {

        public override string ToString() => $"{shipItemParams.rotationSpeed}, {shipItemParams.movementSpeed}, {shipItemParams.movementDamping}, {shipItemParams.maximumSpeed}, {shipItemParams.brakingForce}, {shipItemParams.energyConsumption}";

        public ShipItemParams shipItemParams;

        protected ShipItem(ItemParams itemParams, ShipItemParams shipItemParams) : base(itemParams) {
            this.shipItemParams = shipItemParams;
        }
    }
    
    [Serializable]
    public enum ShipItemType {
        None,
        RearEngine,
        SideEngine,
        Weapon,
        Shield,
        Generator
    }
    [Serializable]
    public struct EnergyConsumption {
        [Serializable]
        public enum ConsumptionMode {
            None,
            PerSecond,
            PerUsage
        }

        public ConsumptionMode mode;
        public float value;

        public override string ToString() => $"{mode.ToString()}: {value}";
        public EnergyConsumption(ConsumptionMode mode = ConsumptionMode.None, float value = 0) {
            this.mode = mode;
            this.value = value;
        }
    }

    [Serializable]
    public struct ShipItemParams {
        public float rotationSpeed;
        public float movementSpeed;
        public float movementDamping;
        public float maximumSpeed;
        public float brakingForce;
        public float armor;
        public EnergyConsumption energyConsumption;

        public ShipItemParams(float armor = 0,
            float rotationSpeed = 0,
            float movementSpeed = 0,
            float movementDamping = 0,
            float maximumSpeed = 0,
            float brakingForce = 0,
            EnergyConsumption energyConsumption = default
        ) {
            this.armor = armor;
            this.rotationSpeed = rotationSpeed;
            this.movementSpeed = movementSpeed;
            this.movementDamping = movementDamping;
            this.maximumSpeed = maximumSpeed;
            this.brakingForce = brakingForce;
            this.energyConsumption = energyConsumption;
        }
    }

    
}