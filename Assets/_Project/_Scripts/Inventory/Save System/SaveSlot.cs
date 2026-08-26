using System;
using Inventory.Item;

namespace Inventory.System
{
    [Serializable]
    public class SaveSlot
    {
        public int slotIndex;
        public ItemData item;
        public int quantity;
    }
}