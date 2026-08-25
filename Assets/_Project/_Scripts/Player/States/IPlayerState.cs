using UnityEngine;

namespace Player
{
    public interface IPlayerState
    {
        void HandleInput();
        void Enter();
        void Update();
        void FixedUpdate();
        void Exit();
    }
}