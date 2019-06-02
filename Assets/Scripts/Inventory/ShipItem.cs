namespace Inventory {
    public enum ShipItemType {
        None,
        Engine,
        Weapon
    }

    public abstract class ShipItem : ItemBase<ShipItemType> { }

    public class ShipWeapon : ShipItem {

        public enum Variant {
            None,
            Laser,
            Kinetic,
            Plasma
        }

        public Variant WeaponVariant;
        public override ShipItemType ItemType => ShipItemType.Weapon;
    }

    public class ShipEngine : ShipItem {
        public enum Variant {
            None,
            Rear,
            Side
        }

        public override ShipItemType ItemType => ShipItemType.Engine;
        public Variant EngineVariant;
    }
}