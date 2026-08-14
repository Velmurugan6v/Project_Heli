using System.Collections.Generic;
using System.Linq;
using HelicopterTag.Core;
using HelicopterTag.Gameplay.Match;
using HelicopterTag.Gameplay.Player;
using HelicopterTag.Gameplay.Tag;
using UnityEngine;

namespace HelicopterTag.Gameplay.Scoring.Strategies
{
    public class SurvivalTimeStrategy : IScoreStrategy, ITickable
    {
        public void Initialize()
        {
            GameLogger.Log("Survival time strategy initialized");
        }

        public void Dispose()
        {
        }

        public MatchResult GetMatchResult(IReadOnlyList<PlayerContext> players)
        {
            var sortedPlayers = players.
                OrderByDescending(players => players.MatchData.SurvivalTime).ToList();

            List<PlayerResult> results = new();

            for (int i = 0; i < sortedPlayers.Count; i++)
            {
                PlayerContext player = sortedPlayers[i];
                PlayerResult result =
                    new PlayerResult(player, ResultType.SurvivalTime, player.MatchData.SurvivalTime, i + 1);
                
                results.Add(result);
            }

            return new MatchResult(results);
        }

        public void Tick(float deltaTime, IReadOnlyList<PlayerContext> players)
        {
            foreach (PlayerContext player in players)
            {
                if (player.MatchData.IsIt) continue;

                player.MatchData.AddSurvivalTime(deltaTime);
            }
        }
    }
}