using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Config;
using HelicopterTag.Gameplay.Events;
using UnityEngine;

namespace HelicopterTag.Gameplay.States
{
    public class PlayingState : IGameState
    {
        private readonly GameplayConfig _config;
        private float _timeRemaining;
        private int _lastDisplayedSecond;
        public bool IsCompleted { get; private set; }


        public PlayingState(GameplayConfig config)
        {
            _config = config;
        }

        public void Enter()
        {
            GameLogger.Log("Enter Playing State");
            IsCompleted = false;
            _timeRemaining = _config.MatchDuration;
            _lastDisplayedSecond = Mathf.CeilToInt(_timeRemaining);
            EventBus.Publish(new MatchStartedEvent());
        }

        public void Exit()
        {
            GameLogger.Log("Exit Playing State");
        }

        public void Tick()
        {
            if (IsCompleted)
                return;

            _timeRemaining -= Time.deltaTime;

            int displayTime = Mathf.CeilToInt(_timeRemaining);

            if (displayTime != _lastDisplayedSecond)
            {
                _lastDisplayedSecond = displayTime;
                EventBus.Publish(new MatchTimeChangedEvent(displayTime));
            }

            if (_timeRemaining <= 0f)
            {
                _timeRemaining = 0;
                IsCompleted = true;
            }
        }
    }
}