using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory {
    public abstract class ItemBase <T> : ScriptableObject where T: Enum {
        public ItemParams ItemParams;
        
        public abstract T ItemType { get; }
        protected ItemBase(ItemParams itemParams) {
            ItemParams = itemParams;
        }
    }
    [Serializable]
    public struct ItemParams {
        public float weight;
        public float price;
        public ItemParams(float weight, float price) {
            this.weight = weight;
            this.price = price;
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