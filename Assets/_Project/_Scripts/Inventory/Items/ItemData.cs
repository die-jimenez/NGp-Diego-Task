using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif

namespace Inventory.Item
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        [Title("Main Data")]
        public string itemName;
        [MultiLineProperty(2)] public string description;
        public ItemType type;

        [Title("Stacks")]
        public bool stackable;
        public int maxStack;

        [Title("Visuals")]
        [PreviewField(40, ObjectFieldAlignment.Right)] public Sprite itemSprite;
        [PreviewField(40, ObjectFieldAlignment.Right)] public Sprite icon;
    }
}