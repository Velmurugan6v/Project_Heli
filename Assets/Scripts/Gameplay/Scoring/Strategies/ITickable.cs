using System.Collections.Generic;
using HelicopterTag.Gameplay.Player;

namespace HelicopterTag.Gameplay.Scoring.Strategies
{
    public interface ITickable
    {
        void Tick(float deltaTime, IReadOnlyList<PlayerContext> players);
    }
}