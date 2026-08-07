using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Player.Event;

namespace HelicopterTag.Gameplay.Player
{
    public class PlayerMatchData
    {
        private PlayerContext _owner;

        public int TagScore { get; private set; }
        public float SurvivalTime { get; private set; }

        public PlayerMatchData(PlayerContext owner)
        {
            _owner = owner;
        }

        public void AddTagScore(int amount)
        {
            TagScore += amount;
            EventBus.Publish(new PlayerScoreChangedEvent(_owner, TagScore));
        }

        public void AddSurvivalTime(float time)
        {
            SurvivalTime += time;
        }

        public void Reset()
        {
            TagScore = 0;
            SurvivalTime = 0f;
        }
    }
}