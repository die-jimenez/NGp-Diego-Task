using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Title("Movement")]
        [SerializeField] private float moveSpeed = 25f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            _rb.linearVelocity = direction * (moveSpeed * Time.fixedDeltaTime);
        }
        
        public void Stop()
        {
            _rb.linearVelocity = Vector2.zero;
        }

    }
}