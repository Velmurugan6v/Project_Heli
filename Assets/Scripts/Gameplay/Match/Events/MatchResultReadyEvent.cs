namespace HelicopterTag.Gameplay.Match.Events
{
    public readonly struct MatchResultReadyEvent
    {
        public MatchResult Result { get; }

        public MatchResultReadyEvent(MatchResult result)
        {
            Result = result;
        }
    }
}