using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.Item
{
    public class DraggableItem: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        
        public void OnBeginDrag(PointerEventData e) {
            // InventoryManager.Instance.draggedSlot = this.slotIndex;
            // canvasGroup.alpha = 0.5f;
        }
    
        public void OnEndDrag(PointerEventData e) {
            // Si soltó sobre otro slot → swap
            // Si soltó fuera → drop/remover
        }

        public void OnDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}