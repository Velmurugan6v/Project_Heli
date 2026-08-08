using System;
using UnityEngine;

namespace HelicopterTag.Gameplay.Player
{
    public class PlayerContext : MonoBehaviour
    {
        private string _displayName;
        public string DisplayName => _displayName;
        public PlayerMatchData MatchData { get; private set; }

        private void Awake()
        {
            MatchData = new PlayerMatchData(this);
        }
    }
}