using System;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Events;
using TMPro;
using UnityEngine;

namespace HelicopterTag.UI.Gameplay
{
    public class MatchTimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;

        private void OnEnable()
        {
            EventBus.Subscribe<MatchTimeChangedEvent>(OnMatchTimeChanged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MatchTimeChangedEvent>(OnMatchTimeChanged);
        }

        private void OnMatchTimeChanged(MatchTimeChangedEvent eventData)
        {
            int minutes = eventData.SecondRemaining / 60;
            int seconds = eventData.SecondRemaining % 60;

            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}