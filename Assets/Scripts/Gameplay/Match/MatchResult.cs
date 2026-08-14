using System.Collections.Generic;
using HelicopterTag.Gameplay.Player;

namespace HelicopterTag.Gameplay.Match
{
    public class MatchResult
    {
        public IReadOnlyList<PlayerResult> Results { get; }
        public PlayerResult Winner => Results.Count > 0 ? Results[0] : null;
        

        public MatchResult(IReadOnlyList<PlayerResult> results)
        {
            Results = results;
        }
    }
}