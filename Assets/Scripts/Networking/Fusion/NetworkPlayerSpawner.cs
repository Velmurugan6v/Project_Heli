using Fusion;
using HelicopterTag.Core;
using UnityEngine;

namespace HelicopterTag.Networking.Fusion
{
    public class NetworkPlayerSpawner : SimulationBehaviour, IPlayerJoined
    {
        [SerializeField] private NetworkObject _playerPrefab;

        public void PlayerJoined(PlayerRef player)
        {
            if (!Runner.IsServer)
                return;

            GameLogger.Log($"[Fusion] Player joined : {player}");
            Runner.Spawn(_playerPrefab, GetSpawnPosition(player), Quaternion.identity, player);
        }

        private Vector3 GetSpawnPosition(PlayerRef player)
        {
            int index = player.RawEncoded % 4;
            return new Vector3(index * 5f, 2f, 0f);
        }
    }
}