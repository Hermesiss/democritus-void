namespace Inventory {
    public enum ShipItemType {
        None,
        Engine,
        Weapon,
        Shield,
        Generator
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

    public class ShipShield : ShipItem {
        public override ShipItemType ItemType => ShipItemType.Shield;
    }

    public class ShipGenerator : ShipItem {
        public override ShipItemType ItemType => ShipItemType.Generator;
    }
}