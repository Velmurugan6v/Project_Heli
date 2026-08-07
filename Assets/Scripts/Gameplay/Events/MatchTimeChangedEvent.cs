using UnityEngine;

namespace HelicopterTag.Gameplay.Events
{
    public class MatchTimeChangedEvent
    {
        public int SecondRemaining { get; }

        public MatchTimeChangedEvent(int secondRemaining)
        {
            SecondRemaining = secondRemaining;
        }
    }
}