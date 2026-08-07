using UnityEngine;

namespace HelicopterTag.Gameplay.Scoring
{
    public interface IScoreStrategy
    {
        void Initialize();
        void Dispose();
    }
}