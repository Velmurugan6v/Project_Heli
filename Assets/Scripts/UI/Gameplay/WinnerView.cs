using System;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Match;
using HelicopterTag.Gameplay.Match.Events;
using TMPro;
using UnityEngine;

namespace HelicopterTag.UI.Gameplay
{
    public class WinnerView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _winnerNameText;
        [SerializeField] private TMP_Text _winnerScoreText;

        private MatchResultFormatter _formatter;

        private void Awake()
        {
            _formatter = new MatchResultFormatter();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<MatchResultReadyEvent>(OnMatchResultReady);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MatchResultReadyEvent>(OnMatchResultReady);
        }

        private void OnMatchResultReady(MatchResultReadyEvent eventData)
        {
            PlayerResult winner = eventData.Result.Winner;

            _panel.SetActive(true);

            _winnerNameText.text = winner.Player.name;
            _winnerScoreText.text = _formatter.Format(winner);
        }
    }
}