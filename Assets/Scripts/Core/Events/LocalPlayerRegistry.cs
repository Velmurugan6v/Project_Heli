using System;

namespace HelicopterTag.Core.Events
{
    public static class LocalPlayerRegistry
    {
        public static ITagAction LocalTagAction { get; private set; }
        public static ITagInputRequest LocalTagInput { get; private set; }

        public static IMatchPhaseAuthority MatchPhaseAuthority { get; private set; }

        public static event Action<ITagAction> LocalTagActionChanged;
        public static event Action<ITagInputRequest> LocalTagInputChanged;

        public static void RegisterLocalTagAction(ITagAction tagAction)
        {
            LocalTagAction = tagAction;
            LocalTagActionChanged?.Invoke(tagAction);
        }

        public static void RegisterTagInput(ITagInputRequest tagInput)
        {
            LocalTagInput = tagInput;
            LocalTagInputChanged?.Invoke(tagInput);
        }

        public static void ClearTagAction()
        {
            LocalTagAction = null;
            LocalTagActionChanged?.Invoke(null);
        }

        public static void ClearTagInput()
        {
            LocalTagInput = null;
            LocalTagInputChanged?.Invoke(null);
        }

        // Match phase authority registration
        public static void RegisterMatchPhaseAuthority(IMatchPhaseAuthority authority)
        {
            MatchPhaseAuthority = authority;
        }

        public static void UnregisterMatchPhaseAuthority(IMatchPhaseAuthority authority)
        {
            if (MatchPhaseAuthority != authority)
                return;

            MatchPhaseAuthority = null;
        }
    }
}