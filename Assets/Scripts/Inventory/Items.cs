using System;
using System.Collections.Generic;

namespace Inventory {
    public abstract class ItemBase <T> where T: Enum {
        public readonly float Weight;
        public readonly float Price;
        public abstract T ItemType { get; }
        protected ItemBase(ItemParams itemParams) {
            Weight = itemParams.Weight;
            Price = itemParams.Price;
        }
    }
    
    public struct ItemParams {
        public readonly float Weight;
        public readonly float Price;
        public ItemParams(float weight, float price) {
            Weight = weight;
            Price = price;
        }
    }

    public interface IItemCollection<out T> : IEnumerable<T> {
        T this[int index] { get; }
        
        T Add(object item, int? index = null);
        T Remove(int index);
        T[] Resize(int newSize);
        
        int Count { get; }
    }
}