using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 moveDirection;

        private void OnMove(InputValue value)
        {
            moveDirection = value.Get<Vector2>().normalized;
        }
    }
}