using System;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Player;
using HelicopterTag.Gameplay.Player.Event;
using TMPro;
using UnityEngine;

namespace HelicopterTag.UI.Gameplay
{
    public class ScoreEntryView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerNameText;
        [SerializeField] private TMP_Text _playerScoreText;

        private PlayerContext _player;

        public void Initialize(PlayerContext player)
        {
            _player = player;
            _playerNameText.text = player.name;
            _playerScoreText.text = "0";
        }

        private void OnEnable()
        {
            EventBus.Subscribe<PlayerScoreChangedEvent>(OnPlayerScoreChanged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<PlayerScoreChangedEvent>(OnPlayerScoreChanged);
        }

        private void OnPlayerScoreChanged(PlayerScoreChangedEvent eventData)
        {
            if (eventData.Player != _player)
                return;

            _playerScoreText.text = eventData.TagScore.ToString();
        }
    }
}