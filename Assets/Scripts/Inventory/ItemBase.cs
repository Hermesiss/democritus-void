using System;
using UnityEngine;

namespace Inventory {
    public abstract class ItemBase <T> : ScriptableObject where T: Enum {
        public ItemParams ItemParams;
        
        public abstract T ItemType { get; }
        
        protected ItemBase(ItemParams itemParams) {
            if (itemParams.gameImage == null) itemParams.gameImage = CommonResources.MissingSprite;
            if (itemParams.inventoryImage == null) itemParams.inventoryImage = CommonResources.MissingSprite;
            ItemParams = itemParams;
        }
    }
}