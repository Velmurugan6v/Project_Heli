using HelicopterTag.Gameplay.Player;

namespace HelicopterTag.Gameplay.Match
{
    public class PlayerResult 
    {
        public PlayerContext Player { get; }
        public int Score { get; }
        public int Rank { get; }

        public PlayerResult(PlayerContext player, int score, int rank)
        {
            Player = player;
            Score = score;
            Rank = rank;
        }
    }
}