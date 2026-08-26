using System;
using System.Collections.Generic;
using Inventory.Item;
using Inventory.Slot;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        [Title("Data")]
        public int maxSlots = 7;
        public int selectedSlot = 0;

        [Title("UI")]
        public List<InventorySlotUI> slotsUI = new();

        [Title("World Drop")]
        [SerializeField] private GameObject worldItemPrefab;


        public int draggedSlot { get; set; }
        private List<InventorySlot> _slots = new();


        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            InitializeInventory();
        }

        private void InitializeInventory()
        {
            for (int i = _slots.Count; i < slotsUI.Count; i++) {
                //TODO: Here is where I get the data to slots
                _slots.Add(new InventorySlot());
                _slots[i].item = slotsUI[i].currentItem;
            }

            RefreshAllUI();
        }

        public void RefreshAllUI()
        {
            for (int i = 0; i < slotsUI.Count; i++) {
                slotsUI[i].Setup(_slots[i].item, _slots[i].stack, i);
            }
        }

        public void RefreshUISlot(int index)
        {
            slotsUI[index].Setup(_slots[index].item, _slots[index].stack, index);
        }


        #region Item Behaviors

        public void SwapSlots(int fromIndex, int toIndex)
        {
            InventorySlot temp = _slots[fromIndex];
            _slots[fromIndex] = _slots[toIndex];
            _slots[toIndex] = temp;

            RefreshUISlot(fromIndex);
            RefreshUISlot(toIndex);
        }

        public void DropItemToWorld(int slotIndex, Vector2 worldPosition)
        {
            if (slotIndex < 0 || slotIndex >= _slots.Count) return;

            InventorySlot slot = _slots[slotIndex];
            if (slot.item == null) return;

            worldItemPrefab = slot.item.droppedPrefab;
            if (!worldItemPrefab) return;

            //Spawn
            GameObject droppedItem = Instantiate(worldItemPrefab, worldPosition, Quaternion.identity);

            // Clean Inventory
            slot.item = null;
            slot.stack = 0;
            RefreshUISlot(slotIndex);
        }

        public bool AddItem(ItemData item, int amount)
        {
            if (item == null || amount <= 0) return false;

            if (item.stackable) {
                for (int i = 0; i < _slots.Count; i++) {
                    if (_slots[i].item == item && _slots[i].stack < item.maxStack) {
                        int spaceLeft = item.maxStack - _slots[i].stack;
                        int amountToAdd = Mathf.Min(amount, spaceLeft);

                        _slots[i].stack += amountToAdd;
                        amount -= amountToAdd;
                        RefreshUISlot(i);

                        // All added
                        if (amount <= 0) return true;
                    }
                }
            }

            //If there is a remaining quantity (or the item isn't stackable), find an empty slot.
            int emptySlotIndex = FindEmptySlotIndex();
            bool hasSpaceInInventory = emptySlotIndex != -1;
            if (hasSpaceInInventory) {
                _slots[emptySlotIndex].item = item;
                _slots[emptySlotIndex].stack = amount;
                RefreshUISlot(emptySlotIndex);
                return true;
            }

            Debug.LogWarning("Inventory Full");
            return false;
        }

        private int FindEmptySlotIndex()
        {
            for (int i = 0; i < _slots.Count; i++) {
                if (_slots[i].item == null) {
                    return i;
                }
            }

            return -1; //Inventory full
        }

        #endregion
    }
}