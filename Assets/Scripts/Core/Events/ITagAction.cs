namespace HelicopterTag.Core.Events
{
    public interface ITagAction
    {
        public bool CanTag { get; }
        void TryTag();
    }
}