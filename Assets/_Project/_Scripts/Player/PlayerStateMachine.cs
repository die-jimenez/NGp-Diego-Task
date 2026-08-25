using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [Title("StateMachine")]
        [SerializeReference] private IPlayerState currentState;

        [Title("Components")]
        public PlayerMovement movement;
        public PlayerInputHandler input;
        public PlayerAnimations animations;


        #region States

        [HideInInspector] public IdleState idleState;
        [HideInInspector] public WalkState walkState;

        #endregion


        private void Start()
        {
            //Components
            if (input == null) input = GetComponent<PlayerInputHandler>();
            if (movement == null) movement = GetComponent<PlayerMovement>();
            if (animations == null) animations = GetComponentInChildren<PlayerAnimations>();

            //States
            idleState = new IdleState(this);
            walkState = new WalkState(this);
            ChangeState(idleState);
        }

        private void OnDisable()
        {
            currentState?.Exit();
            currentState = null;
        }

        private void Update()
        {
            currentState?.HandleInput();
            currentState?.Update();
        }

        private void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        private void LateUpdate()
        {
            currentState?.LateUpdate();
        }

        public void ChangeState(IPlayerState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
    }
}