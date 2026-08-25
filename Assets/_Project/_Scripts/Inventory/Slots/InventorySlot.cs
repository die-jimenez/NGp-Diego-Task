using Inventory.Item;

namespace Inventory.Slot
{
    [System.Serializable]
    public class InventorySlot {
        public ItemData item;
        public int quantity;
        public bool isEmpty => item == null;
    }
}