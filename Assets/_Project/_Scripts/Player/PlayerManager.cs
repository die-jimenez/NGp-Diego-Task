using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("StateMachine")]
        [SerializeReference] private IPlayerState currentState;

        [Header("Components")]
        public PlayerMovement movement; 
        
        #region States

        [HideInInspector] public IdleState idleState;
        [HideInInspector] public WalkState walkState;

        #endregion


        private void Start()
        {
            //Components
            movement = !movement ? GetComponent<PlayerMovement>() : movement;
            
            //States
            idleState = new IdleState();
            walkState = new WalkState(movement);
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

        public void ChangeState(IPlayerState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
    }
}