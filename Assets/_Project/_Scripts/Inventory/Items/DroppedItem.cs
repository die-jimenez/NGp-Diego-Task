using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory.Item
{
    public class DroppedItem : MonoBehaviour
    {
        [Title("Data about item")]
        public ItemData itemData;
        public int quantity = 1;

        private bool _isFollwignPlayer;
        private Transform _playerTransform;
        private float _pickupSpeed = 5;


        private void Update()
        {
            if (_isFollwignPlayer) {
                Vector3 targetPos = new Vector3(_playerTransform.position.x, _playerTransform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, _pickupSpeed * Time.deltaTime);
            }
        }


        #region API For Player

        public void PickUp(Transform playerTransform)
        {
            bool success = InventoryManager.Instance.AddItem(itemData, quantity);
            if (!success) {
                transform.DOShakePosition(0.3f, strength: 0.15f, vibrato: 10);
                return;
            }

            GetComponent<Collider2D>().enabled = false;
            _playerTransform = playerTransform;
            _isFollwignPlayer = true;
            transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.4f).SetEase(Ease.OutBack).OnComplete(() => {
                //TODO: Should be here where and when item is added, but I don't have too much time
                //InventoryManager.Instance.AddItem(itemData, quantity);
                Destroy(gameObject);
            });
        }

        #endregion
    }
}