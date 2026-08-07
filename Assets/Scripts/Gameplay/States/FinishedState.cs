using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Events;

namespace HelicopterTag.Gameplay.States
{
    public class FinishedState : IGameState
    {
        public bool IsCompleted { get; private set; }
        

        public void Enter()
        {
            GameLogger.Log("Enter Final State");
            IsCompleted = false;
            EventBus.Publish(new MatchFinishedEvent());
        }

        public void Exit()
        {
            GameLogger.Log("Exit Final State");
        }

        public void Tick()
        {
            
        }
    }
}
