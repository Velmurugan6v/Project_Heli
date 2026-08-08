using System.Collections.Generic;
using HelicopterTag.Gameplay.Match;
using HelicopterTag.Gameplay.Player;
using UnityEngine;

namespace HelicopterTag.Gameplay.Scoring
{
    public interface IScoreStrategy
    {
        void Initialize();
        void Dispose();

        MatchResult GetMatchResult(IReadOnlyList<PlayerContext> players);
    }
}