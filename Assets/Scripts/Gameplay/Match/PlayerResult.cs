using HelicopterTag.Gameplay.Player;

namespace HelicopterTag.Gameplay.Match
{
    public class PlayerResult 
    {
        public PlayerContext Player { get; }
        public ResultType Type { get; }
        public float Value { get; }
        public int Rank { get; }

        public PlayerResult(PlayerContext player,ResultType type, float value, int rank)
        {
            Player = player;
            Type = type;
            Value = value;
            Rank = rank;
        }
    }
}