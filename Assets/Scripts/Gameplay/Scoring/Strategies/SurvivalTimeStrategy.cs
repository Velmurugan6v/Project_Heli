using System.Collections.Generic;
using HelicopterTag.Core;
using HelicopterTag.Gameplay.Match;
using HelicopterTag.Gameplay.Player;
using HelicopterTag.Gameplay.Tag;
using UnityEngine;

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

        public MatchResult GetMatchResult(IReadOnlyList<PlayerContext> players)
        {
            PlayerContext winner = null;
            float highestTime=float.MinValue;

            foreach (PlayerContext player in players)
            {
                if (player.MatchData.SurvivalTime > highestTime)
                {
                    winner = player;
                    highestTime = player.MatchData.SurvivalTime;
                }
            }
            
            return new MatchResult(winner, Mathf.RoundToInt(highestTime));
            
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