using UnityEngine;

namespace HelicopterTag.Gameplay.States
{
    public interface IGameState
    {
        bool IsCompleted { get; }
        void Enter();
        void Exit();
        void Tick();
    }
}