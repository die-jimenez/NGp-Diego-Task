using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Inventory.Item;
using Sirenix.OdinInspector;
using TMPro;


namespace Inventory.Slot
{
    public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        [Title("UI Main References")]
        public Image iconImage;
        public RawImage stackContainer;
        public TextMeshProUGUI stackText;
        public CanvasGroup slotCanvasGroup;
        
        [Title("UI Main References")]
        public Image hoverContainer;
        public TextMeshProUGUI hoverText;
        public CanvasGroup hoverCanvasGroup;
        

        [Title("Slot Data")]
        public int slotIndex;
        public ItemData currentItem;
        public int stackQuantity;


        //Privates 
        private Transform _originalIconParent;
        private Vector3 _originalIconLocalPos;
        private string _blockedTag = "Blocked";
        private Tween hoverTween;


        public void Setup(ItemData item, int qty, int index)
        {
            slotIndex = index;
            currentItem = item;
            stackQuantity = qty;

            if (item != null) {
                iconImage.sprite = item.icon;
                iconImage.color = Color.white;
                hoverText.text = item.description;

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


        #region Hover enter and exit Events

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (currentItem == null || hoverContainer == null) return;

            // Kill previous tween to prevent overlapping animations on rapid mouse movement
            hoverTween?.Kill();
            hoverContainer.transform.DOKill();

            hoverContainer.gameObject.SetActive(true);
            hoverCanvasGroup.alpha = 0f;
            hoverContainer.transform.localScale = Vector3.one * 0.85f;

            hoverCanvasGroup.DOFade(1f, 0.15f);
            hoverContainer.transform.DOScale(1f, 0.15f).SetEase(Ease.OutBack);

            // Infinite floating effect 
            hoverContainer.transform.DOLocalMoveY(2, 1f)
                .SetRelative()
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (hoverContainer == null) return;

            hoverTween?.Kill();
            hoverContainer.transform.DOKill();

            hoverTween = DOTween.Sequence()
                .Append(hoverCanvasGroup.DOFade(0f, 0.1f))
                .Join(hoverContainer.transform.DOScale(0.9f, 0.1f))
                .OnComplete(() => hoverContainer.gameObject.SetActive(false));
        }

        #endregion


        #region Drag and Drop Events

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (currentItem == null) return;
            _originalIconParent = iconImage.transform.parent;
            _originalIconLocalPos = iconImage.transform.localPosition;

            //Get out the icon from Layout
            iconImage.transform.SetParent(transform.root);
            iconImage.raycastTarget = false;
            slotCanvasGroup.alpha = 0.4f;
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
            slotCanvasGroup.alpha = 1f;

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

        #endregion
    }
}