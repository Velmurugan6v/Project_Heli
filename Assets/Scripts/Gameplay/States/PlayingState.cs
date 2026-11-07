using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Config;
using HelicopterTag.Gameplay.Events;
using UnityEngine;

namespace HelicopterTag.Gameplay.States
{
    public class PlayingState : IGameState
    {
        readonly GameplayConfig m_config;
        float m_timeRemaining;
        int m_lastDisplayedSecond;
        public bool IsCompleted { get; private set; }


        public PlayingState(GameplayConfig mConfig)
        {
            m_config = mConfig;
        }

        public void Enter()
        {
            GameLogger.Log("Enter Playing State");
            IsCompleted = false;
            m_timeRemaining = m_config.MatchDuration;
            m_lastDisplayedSecond = Mathf.CeilToInt(m_timeRemaining);
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

            m_timeRemaining -= Time.deltaTime;

            int displayTime = Mathf.CeilToInt(m_timeRemaining);

            if (displayTime != m_lastDisplayedSecond)
            {
                m_lastDisplayedSecond = displayTime;
                EventBus.Publish(new MatchTimeChangedEvent(displayTime));
            }

            if (m_timeRemaining <= 0f)
            {
                m_timeRemaining = 0;
                IsCompleted = true;
            }
        }
    }
}