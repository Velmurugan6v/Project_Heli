using System;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
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
            _panel.SetActive(true);
            GameLogger.Log("Match Result Ready");
            _winnerNameText.text = eventData.Result.Winner.name;
            _winnerScoreText.text = $"{eventData.Result.WinningScore}";
        }
    }
}