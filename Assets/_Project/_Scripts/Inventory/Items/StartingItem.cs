using System;

namespace Inventory.Item
{
    [Serializable]
    public struct StartingItem
    {
        public ItemData item;
        public int quantity;
        public int slotIndex;
    }
}