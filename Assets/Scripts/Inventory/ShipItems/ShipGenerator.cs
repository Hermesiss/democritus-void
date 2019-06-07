using Inventory.ShipItems;
using UnityEngine;

namespace Inventory {
    [CreateAssetMenu(fileName = "New ShipGenerator", menuName = "Game/ShipGenerator", order = 51)]
    public class ShipGenerator : ShipItem {
        public float GenerationSpeed;
        public float EnergyCapacity;
        public ShipGenerator(ItemParams itemParams, ShipItemParams shipItemParams, 
            float generationSpeed, float energyCapacity) : base(itemParams, shipItemParams) {
            GenerationSpeed = generationSpeed;
            EnergyCapacity = energyCapacity;
        }
        public override ShipItemType ItemType => ShipItemType.Generator;
    }
}