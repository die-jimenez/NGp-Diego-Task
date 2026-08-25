using System;

namespace Player
{
    [Serializable]
    public class IdleState: IPlayerState
    {
        private PlayerStateMachine _stateMachine;
        private PlayerMovement _movement;
        private PlayerInputHandler _input;
        private PlayerAnimations _animations;

        public IdleState(PlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _animations = stateMachine.animations;
            _movement = stateMachine.movement;
            _input = stateMachine.input;
        }
        
        public void Enter()
        {
            _movement.Stop();
            _animations.PlayIdle();
        }
        
        public void Exit()
        {
        }
        
        public void HandleInput()
        {
            if (_input.moveDirection.magnitude > 0.1f)
            {
                _stateMachine.ChangeState(_stateMachine.walkState);
            }
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }

        public void LateUpdate()
        {
            
        }
    }
}