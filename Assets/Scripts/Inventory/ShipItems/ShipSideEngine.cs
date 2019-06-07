using UnityEngine;

namespace Inventory.ShipItems {
    [CreateAssetMenu(fileName = "New ShipSideEngine", menuName = "Game/ShipSideEngine", order = 51)]
    public class ShipSideEngine : ShipItem {
        public ShipSideEngine(ItemParams itemParams, ShipItemParams shipItemParams) : base(itemParams, shipItemParams) {
            
        }
        public override ShipItemType ItemType => ShipItemType.SideEngine;
    }
}