using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Inventory.Item;
using Sirenix.OdinInspector;
using TMPro;


namespace Inventory.Slot
{
    public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [Title("UI References")]
        public Image iconImage;
        public RawImage stackContainer;
        public TextMeshProUGUI stackText;
        public CanvasGroup canvasGroup;

        [Title("Slot Data")]
        public int slotIndex;
        public ItemData currentItem;
        public int stackQuantity;


        //Privates 
        private Transform _originalIconParent;
        private Vector3 _originalIconLocalPos;
        private string _blockedTag = "Blocked";


        public void Setup(ItemData item, int qty, int index)
        {
            slotIndex = index;
            currentItem = item;
            stackQuantity = qty;

            if (item != null) {
                iconImage.sprite = item.icon;
                iconImage.color = Color.white;
                if (item.stackable) {
                    stackText.text = stackQuantity.ToString();
                    stackContainer.transform.gameObject.SetActive(true);
                }
                else stackContainer.transform.gameObject.SetActive(false);
            }
            else {
                iconImage.sprite = null;
                iconImage.color = Color.clear;
                stackContainer.transform.gameObject.SetActive(false);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (currentItem == null) return;
            _originalIconParent = iconImage.transform.parent;
            _originalIconLocalPos = iconImage.transform.localPosition;

            //Get out the icon from Layout
            iconImage.transform.SetParent(transform.root);
            iconImage.raycastTarget = false;
            canvasGroup.alpha = 0.4f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (currentItem == null) return;
            iconImage.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (currentItem == null) return;

            // Return icon to Layout
            iconImage.transform.SetParent(_originalIconParent);
            iconImage.transform.localPosition = _originalIconLocalPos;

            iconImage.raycastTarget = true;
            canvasGroup.alpha = 1f;

            // Swap slot
            if (eventData.pointerEnter != null) {
                InventorySlotUI targetSlot = eventData.pointerEnter.GetComponentInParent<InventorySlotUI>();

                if (targetSlot != null && targetSlot != this) {
                    InventoryManager.Instance.SwapSlots(this.slotIndex, targetSlot.slotIndex);
                }
            }

            //Drop
            TryToDropOnWorld(eventData.position);
        }

        public void OnDrop(PointerEventData eventData)
        {
        }

        private void TryToDropOnWorld(Vector2 screenPosition)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0f));
            Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

            RaycastHit2D hit = Physics2D.Raycast(worldPos2D, Vector2.zero);

            if (hit.collider != null) {
                //Drop on blocked tile
                if (hit.collider.CompareTag(_blockedTag) || hit.collider.CompareTag("Player")) {
                    //TODO: ADD SOUND
                    return;
                }

                InventoryManager.Instance.DropItemToWorld(this.slotIndex, worldPos2D);
            }
            else Debug.Log("Tile with no collider");
        }
    }
}