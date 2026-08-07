using HelicopterTag.Gameplay.Scoring;
using UnityEngine;

namespace HelicopterTag.Gameplay.Config
{
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Helicopter Tag/Gameplay Config")]
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float CountDownDuration { get; private set; }
        [field: SerializeField, Min(1)] public float MatchDuration { get; private set; }
        [field: SerializeField, Min(0)] public float TagProtectionDuration { get; private set; }

        [field: SerializeField] public ScoringMode ScoringMode { get; private set; }
    }
}