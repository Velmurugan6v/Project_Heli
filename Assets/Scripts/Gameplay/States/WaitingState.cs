using HelicopterTag.Core;
using HelicopterTag.Gameplay.Input;

namespace HelicopterTag.Gameplay.States
{
    public class WaitingState : IGameState
    {
        private readonly IMatchInput _matchInput;


        public bool IsCompleted { get; private set; }

        public WaitingState(IMatchInput matchInput)
        {
            _matchInput = matchInput;
        }


        public void Enter()
        {
            GameLogger.Log("--Enter Waiting State");
            IsCompleted = false;
        }

        public void Exit()
        {
            GameLogger.Log("--Exit Waiting State");
        }

        public void Tick()
        {
            if (_matchInput.StartPressed)
                IsCompleted = true;
        }
    }
}