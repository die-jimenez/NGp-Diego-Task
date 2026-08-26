using System.Collections.Generic;
using UnityEngine;
using Inventory.Item;

namespace Inventory.System
{
    public static class SaveSystem
    {
        private const string SAVE_KEY = "InventoryData";

        public static void SaveInventory()
        {
            List<SaveSlot> slotsToSave = new List<SaveSlot>();

            var slots = InventoryManager.Instance.GetSlots();
            for (int i = 0; i < slots.Count; i++) {
                if (slots[i].item == null) continue;

                slotsToSave.Add(new SaveSlot
                {
                    slotIndex = i,
                    item = slots[i].item,
                    quantity = slots[i].stack
                });
            }

            ES3.Save(SAVE_KEY, slotsToSave);
            Debug.Log("[SaveSystem] Inventory saved.");
        }

        public static void LoadInventory()
        {
            List<SaveSlot> loadedSlots = ES3.Load(SAVE_KEY, new List<SaveSlot>());

            InventoryManager.Instance.ClearInventory();

            foreach (var saveSlot in loadedSlots) {
                if (saveSlot.item == null) {
                    Debug.LogWarning($"[SaveSystem] FAILED to load item at slot {saveSlot.slotIndex}: reference is null.");
                    continue;
                }

                InventoryManager.Instance.AddItem(saveSlot.item, saveSlot.quantity, saveSlot.slotIndex);
            }

            InventoryManager.Instance.RefreshAllUI();
            Debug.Log("[SaveSystem] Inventory loaded.");
        }
    }
}