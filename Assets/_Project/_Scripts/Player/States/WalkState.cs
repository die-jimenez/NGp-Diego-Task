using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class WalkState : IPlayerState
    {
        private PlayerStateMachine _stateMachine;
        private PlayerMovement _movement;
        private PlayerInputHandler _input;
        private PlayerAnimations _animations;

        private Vector2 _direction;

        public WalkState(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _animations = stateMachine.animations;
            _movement = stateMachine.movement;
            _input = stateMachine.input;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            if (_input.moveDirection.magnitude < 0.1f) {
                _stateMachine.ChangeState(_stateMachine.idleState);
                return;
            }

            _direction = SnapToDirection(_input.moveDirection);
        }


        public void Update()
        {
        }

        public void FixedUpdate()
        {
            _movement.Move(_direction);
        }

        public void LateUpdate()
        {
            _animations.PlayWalk(_direction);
        }


        private Vector2 SnapToDirection(Vector2 input)
        {
            if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
                return input.y > 0 ? Vector2.up : Vector2.down;
            else
                return input.x > 0 ? Vector2.right : Vector2.left;
        }
    }
}