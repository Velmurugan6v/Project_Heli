using System;
using System.Collections.Generic;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Events;
using UnityEngine;

namespace HelicopterTag.Gameplay.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerRegistry _registry;
        public IReadOnlyList<PlayerContext> Players => _registry.Players;

        private void Awake()
        {
            _registry = new PlayerRegistry();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<MatchResetEvent>(OnMatchEvent);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MatchResetEvent>(OnMatchEvent);
        }

        private void Start()
        {
            GameLogger.Log($"Registered Players : {Players.Count}");
        }

        private void OnMatchEvent(MatchResetEvent matchResetEvent)
        {
            ResetPlayers();
        }

        public void RegisterPlayer(PlayerContext player)
        {
            _registry.RegisterPlayer(player);
        }

        public void UnregisterPlayer(PlayerContext player)
        {
            _registry.UnregisterPlayer(player);
        }

        public void ResetPlayers()
        {
            foreach (PlayerContext player in Players)
            {
                player.MatchData.Reset();
            }
        }
    }
}