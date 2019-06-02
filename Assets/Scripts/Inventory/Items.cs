using System;

namespace Inventory {
    public abstract class ItemBase <T> where T: Enum{
        public float Weight { get; private set; }
        public float Price { get; private set; }
        public abstract T ItemType { get; }
    }
}