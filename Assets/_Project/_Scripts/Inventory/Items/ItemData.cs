using UnityEngine;

namespace Inventory.Item
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite itemSprite;
        public Sprite icon;
        public ItemType type;
        public int maxStack;
        public string description;
    }
}