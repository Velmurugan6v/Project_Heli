namespace HelicopterTag.Gameplay.Player.Event
{
    public readonly struct PlayerScoreChangedEvent
    {
        public PlayerContext Player { get; }
        public int TagScore { get; }

        public PlayerScoreChangedEvent(PlayerContext player, int tagScore)
        {
            Player = player;
            TagScore = tagScore;
        }
    }
}