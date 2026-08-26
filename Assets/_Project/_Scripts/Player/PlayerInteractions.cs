using Inventory.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out DroppedItem worldItem)) {
                worldItem.PickUp(transform);
            }
        }
    }
}