using System;
using UnityEngine;

namespace HelicopterTag.Gameplay.Player
{
    public class PlayerContext : MonoBehaviour
    {
        public PlayerMatchData MatchData { get; private set; }

        private void Awake()
        {
            MatchData = new PlayerMatchData(this);
        }
    }
}