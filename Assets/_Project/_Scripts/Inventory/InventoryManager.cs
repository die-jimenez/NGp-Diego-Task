using System;
using System.Collections.Generic;
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
        
        public void SwapSlots(int fromIndex, int toIndex)
        {
            InventorySlot temp = _slots[fromIndex];
            _slots[fromIndex] = _slots[toIndex];
            _slots[toIndex] = temp;
            
            RefreshUISlot(fromIndex);
            RefreshUISlot(toIndex);
        }
    }
}