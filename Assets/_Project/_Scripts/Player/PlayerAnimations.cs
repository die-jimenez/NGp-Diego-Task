using UnityEngine;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        private string _lastDirection = "Down";

        private void Awake()
        {
            if (anim == null) anim = GetComponent<Animator>();
        }

        public void PlayIdle()
        {
            if (!anim) return;
            anim.Play(GetIdleHash());
        }

        public void PlayWalk(Vector2 direction)
        {
            if (!anim) return;
            UpdateDirection(direction);
            anim.Play(GetWalkHash());
        }

        private void UpdateDirection(Vector2 direction)
        {
            if (direction.y > 0) _lastDirection = "Up";
            else if (direction.y < 0) _lastDirection = "Down";
            else if (direction.x > 0) _lastDirection = "Right";
            else if (direction.x < 0) _lastDirection = "Left";
        }

        private int GetIdleHash() => Animator.StringToHash("Clip_Player_Idle" + _lastDirection);
        private int GetWalkHash() => Animator.StringToHash("Clip_Player_Walk" + _lastDirection);
    }
}