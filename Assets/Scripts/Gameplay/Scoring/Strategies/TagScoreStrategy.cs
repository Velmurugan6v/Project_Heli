using System.Collections.Generic;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Match;
using HelicopterTag.Gameplay.Player;
using HelicopterTag.Gameplay.Tag.Events;

namespace HelicopterTag.Gameplay.Scoring.Strategies
{
    public class TagScoreStrategy : IScoreStrategy
    {
        public void Initialize()
        {
            EventBus.Subscribe<ItChangedEvent>(OnItChanged);
            GameLogger.Log("Tag Score Strategy Initialized");
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<ItChangedEvent>(OnItChanged);
        }

        public MatchResult GetMatchResult(IReadOnlyList<PlayerContext> players)
        {
            PlayerContext winner = null;
            int highestScore = int.MinValue;

            foreach (PlayerContext player in players)
            {
                if (player.MatchData.TagScore > highestScore)
                {
                    highestScore = player.MatchData.TagScore;
                    winner = player;
                }
            }
            
            return new MatchResult(winner, highestScore);
        }

        private void OnItChanged(ItChangedEvent eventData)
        {
            if (eventData.PreviousIt == null)
                return;

            PlayerContext player = eventData.PreviousIt.PlayerContext;
            player.MatchData.AddTagScore(1);

            GameLogger.Log($"{player.name} : Scored a tag! {player.MatchData.TagScore}");
        }
    }
}