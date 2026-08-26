using Fusion;

namespace HelicopterTag.Networking.Fusion
{
    public static class FusionAuthority
    {
        public static bool IsStateAuthority(NetworkRunner runner)
        {
            return runner != null && runner.IsServer;
        }
    }
}