namespace Inventory {
    public enum CargoItemType {
        None,
        Ore,
        Goods
    }

    public struct CargoItemParams {
        
    }

    public abstract class CargoItem : ItemBase<CargoItemType> {
        protected CargoItem(ItemParams itemParams, CargoItemParams cargoItemParams) : base(itemParams) { }
    }
}