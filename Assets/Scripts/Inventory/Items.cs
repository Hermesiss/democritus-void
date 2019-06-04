using System;
using System.Collections.Generic;

namespace Inventory {
    public abstract class ItemBase <T> where T: Enum{
        public float Weight { get; private set; }
        public float Price { get; private set; }
        public abstract T ItemType { get; }
    }

    public interface IItemCollection<out T> : IEnumerable<T> {
        T this[int index] { get; }
        
        T Add(object item, int? index = null);
        T Remove(int index);
        T[] Resize(int newSize);
        
        int Count { get; }
    }
}