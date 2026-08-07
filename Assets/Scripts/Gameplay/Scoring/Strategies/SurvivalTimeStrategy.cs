using System.Collections.Generic;
using HelicopterTag.Core;
using HelicopterTag.Gameplay.Tag;

namespace HelicopterTag.Gameplay.Scoring.Strategies
{
    public class SurvivalTimeStrategy : IScoreStrategy, ITickable
    {
        private readonly IReadOnlyList<TagParticipant> _participants;


        public SurvivalTimeStrategy(IReadOnlyList<TagParticipant> participants)
        {
            _participants = participants;
        }

        public void Initialize()
        {
            GameLogger.Log("Survival time strategy initialized");
        }

        public void Dispose()
        {
        }

        public void Tick(float deltaTime)
        {
            foreach (TagParticipant participant in _participants)
            {
                if (participant.IsIt) continue;

                participant.PlayerContext.MatchData.AddSurvivalTime(deltaTime);
            }
        }
    }
}