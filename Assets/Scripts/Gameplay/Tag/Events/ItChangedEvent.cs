using UnityEngine;

namespace HelicopterTag.Gameplay.Tag.Events
{
    public readonly struct ItChangedEvent
    {
        public TagParticipant PreviousIt { get; }
        public TagParticipant CurrentIt { get; }

        public ItChangedEvent(TagParticipant previousIt, TagParticipant currentIt)
        {
            PreviousIt = previousIt;
            CurrentIt = currentIt;
        }
    }
}