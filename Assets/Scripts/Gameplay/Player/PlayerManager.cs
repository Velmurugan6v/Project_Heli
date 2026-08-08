using System;
using System.Collections.Generic;
using HelicopterTag.Core;
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

        private void Start()
        {
            GameLogger.Log($"Registered Players : {Players.Count}");
        }

        public void RegisterPlayer(PlayerContext player)
        {
            _registry.RegisterPlayer(player);
        }

        public void UnregisterPlayer(PlayerContext player)
        {
            _registry.UnregisterPlayer(player);
        }
    }
}