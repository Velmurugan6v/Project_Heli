using UnityEngine;

namespace HelicopterTag.Gameplay.Events
{
    public readonly struct CountdownChangedEvent
    {
        public int Value { get; }

        public CountdownChangedEvent(int value)
        {
            Value = value;
        }
    }
}
