using System.Collections.Generic;

namespace HelicopterTag.Gameplay.Player
{
    public class PlayerRegistry
    {
        private readonly List<PlayerContext> _players = new();

        public IReadOnlyList<PlayerContext> Players => _players;

        public void RegisterPlayer(PlayerContext player)
        {
            if (_players.Contains(player))
                return;

            _players.Add(player);
        }

        public void UnregisterPlayer(PlayerContext player)
        {
            _players.Remove(player);
        }
    }
}