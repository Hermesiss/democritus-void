namespace Inventory {
    public enum CargoItemType {
        None,
        Ore,
        Goods
    }

    public abstract class CargoItem : ItemBase<CargoItemType> { }
}