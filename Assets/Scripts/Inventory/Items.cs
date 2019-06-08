using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory {
    //TODO make CustomPropertyDrawer
    [Serializable]
    public struct ItemParams {
        public float weight;
        public float price;
        public Sprite gameImage;
        public Sprite inventoryImage;
        
        public override string ToString() => 
            $"{weight}, " +
            $"{price}, " +
            $"{gameImage}, " +
            $"{inventoryImage}";
        public ItemParams(float weight, float price, Sprite gameImage, Sprite inventoryImage) {
            this.weight = weight;
            this.price = price;
            this.gameImage = gameImage == null ? CommonResources.MissingSprite : gameImage;
            this.inventoryImage = inventoryImage == null ? CommonResources.MissingSprite : inventoryImage;
        }
    }
    
    public interface IItemCollection<out T> : IEnumerable<T> {
        T this[int index] { get; }
        
        /// <summary>
        /// Adds item, returning old item is there was one
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index">Index where to place, first empty or last filled if null</param>
        /// <returns>Old item or null</returns>
        T Add(object item, int? index = null);
        
        /// <summary>
        /// Removes item from index
        /// </summary>
        /// <param name="index">Slot from where to take</param>
        /// <returns>Existed item or null</returns>
        T Remove(int index);
        
        /// <summary>
        /// Changes usable part of an array
        /// </summary>
        /// <param name="newSize"></param>
        /// <returns>Extra objects that were in deleted slots</returns>
        T[] Resize(int newSize);
        
        /// <summary>
        /// Size of usable part
        /// </summary>
        int Count { get; }
    }
}