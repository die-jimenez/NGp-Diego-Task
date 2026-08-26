using System;
using Inventory.Item;

namespace Inventory.Slot
{
    [Serializable]
    public class InventorySlot {
        public ItemData item;
        public int stack;
        public bool isEmpty => item == null;
    }
}