using HelicopterTag.Gameplay.Player;

namespace HelicopterTag.Gameplay.Tag.Events
{
    public readonly struct PlayerTaggedEvent
    {
        public readonly PlayerContext Tagger;
        public readonly PlayerContext Target;

        public PlayerTaggedEvent(PlayerContext tagger, PlayerContext target)
        {
            Tagger = tagger;
            Target = target;
        }
    }
}