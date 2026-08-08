using HelicopterTag.Gameplay.Player;
using UnityEngine;

namespace HelicopterTag.Gameplay.Match
{
    public class MatchResult
    {
        public PlayerContext Winner { get; }
        public int WinningScore { get; }

        public MatchResult(PlayerContext winner, int winningScore)
        {
            Winner = winner;
            WinningScore = winningScore;
        }
    }
}