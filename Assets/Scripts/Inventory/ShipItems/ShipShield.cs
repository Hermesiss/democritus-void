using Inventory.ShipItems;
using UnityEngine;

namespace Inventory {
    [CreateAssetMenu(fileName = "New ShipShield", menuName = "Game/ShipShield", order = 51)]
    public class ShipShield : ShipItem {
        public float DamageCapacity;
        public float RecoverySpeed;
        public ShipShield(ItemParams itemParams, ShipItemParams shipItemParams, 
            float damageCapacity, float recoverySpeed) : base(itemParams, shipItemParams) {
            DamageCapacity = damageCapacity;
            RecoverySpeed = recoverySpeed;
        }
        public override ShipItemType ItemType => ShipItemType.Shield;
    }
}