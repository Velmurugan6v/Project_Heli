using UnityEngine;

namespace HelicopterTag.Gameplay.Tag.Events
{
    public readonly struct TagProtectionChangedEvent
    {
        public TagParticipant Participant { get; }
        public bool IsProtected { get; }

        public TagProtectionChangedEvent(TagParticipant participant, bool isProtected)
        {
            Participant = participant;
            IsProtected = isProtected;
        }
    }
}