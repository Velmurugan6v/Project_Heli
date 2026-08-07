using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Config;
using HelicopterTag.Gameplay.Events;
using UnityEngine;

namespace HelicopterTag.Gameplay.States
{
    public class CountdownState : IGameState
    {
        public bool IsCompleted { get; private set; }
        private readonly GameplayConfig _config;

        private float _timeRemaining;
        private int _lastDisplayedTime;


        public CountdownState(GameplayConfig config)
        {
            _config = config;
        }

        public void Enter()
        {
            GameLogger.Log("Enter Countdown State");
            IsCompleted = false;
            _timeRemaining = _config.CountDownDuration;
            _lastDisplayedTime = -1;
        }

        public void Exit()
        {
            GameLogger.Log("Exit Countdown State");
        }

        public void Tick()
        {
            if (IsCompleted)
                return;

            int displayedTime = Mathf.CeilToInt(_timeRemaining);

            if (displayedTime != _lastDisplayedTime)
            {
                _lastDisplayedTime = displayedTime;
                EventBus.Publish(new CountdownChangedEvent(displayedTime));
            }

            _timeRemaining -= Time.deltaTime;

            if (_timeRemaining <= 0)
            {
                _timeRemaining = 0;
                IsCompleted = true;
            }
        }
    }
}