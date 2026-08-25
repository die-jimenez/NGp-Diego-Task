using UnityEngine;

namespace Player
{
    public interface IPlayerState
    {
        void Enter();
        void Exit();
        void HandleInput();
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}