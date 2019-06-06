using System;

namespace Inventory {
    [Serializable]
    public enum ShipItemType {
        None,
        RearEngine,
        SideEngine,
        Weapon,
        Shield,
        Generator
    }

    public struct EnergyConsumption {
        [Serializable]
        public enum Mode {
            None,
            PerSecond,
            PerUsage
        }

        public Mode ConsumptionMode;
        public float ConsumptionValue;

        public override string ToString() => $"{ConsumptionMode.ToString()}: {ConsumptionValue}";
        public EnergyConsumption(Mode consumptionMode = Mode.None, float consumptionValue = 0) {
            ConsumptionMode = consumptionMode;
            ConsumptionValue = consumptionValue;
        }
    }

    [Serializable]
    public struct ShipItemParams {
        public float rotationSpeed;
        public float movementSpeed;
        public float movementDamping;
        public float maximumSpeed;
        public float brakingForce;
        public EnergyConsumption energyConsumption;

        public ShipItemParams(float rotationSpeed = 0,
            float movementSpeed = 0,
            float movementDamping = 0,
            float maximumSpeed = 0,
            float brakingForce = 0,
            EnergyConsumption energyConsumption = default
        ) {
            this.rotationSpeed = rotationSpeed;
            this.movementSpeed = movementSpeed;
            this.movementDamping = movementDamping;
            this.maximumSpeed = maximumSpeed;
            this.brakingForce = brakingForce;
            this.energyConsumption = energyConsumption;
        }
    }

    public abstract class ShipItem : ItemBase<ShipItemType> {

        public override string ToString() => $"{shipItemParams.rotationSpeed}, {shipItemParams.movementSpeed}, {shipItemParams.movementDamping}, {shipItemParams.maximumSpeed}, {shipItemParams.brakingForce}, {shipItemParams.energyConsumption}";

        public ShipItemParams shipItemParams;

        protected ShipItem(ItemParams itemParams, ShipItemParams shipItemParams) : base(itemParams) {
            this.shipItemParams = shipItemParams;
        }
    }
}