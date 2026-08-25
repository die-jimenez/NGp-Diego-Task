using System;
using System.Collections.Generic;
using Inventory.Slot;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        public List<InventorySlot> slots = new();
        public int maxSlots = 7;
        public int selectedSlot = 0;


        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
    }
}