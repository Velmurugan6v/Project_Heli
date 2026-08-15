using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using HelicopterTag.Core;
using UnityEngine;

namespace HelicopterTag.Networking.Fusion
{
    public class FusionLauncher : MonoBehaviour,INetworkRunnerCallbacks
    {
        [SerializeField] private GameMode _gameMode;
        private NetworkRunner _runner;

        private async void Start()
        {
            GameLogger.Log($"[Fusion] Starting NetworkRunner as {_gameMode}");

            _runner = gameObject.AddComponent<NetworkRunner>();
            
            _runner.AddCallbacks(this);

            var result = await _runner.StartGame(
                new StartGameArgs
                {
                    GameMode = _gameMode,
                    SessionName = "HelicopterTagRoom"
                });

            if (result.Ok)
            {
                GameLogger.Log($"[Fusion] {_gameMode} started successfully.");
                GameLogger.Log($"[Fusion] Session : {_runner.SessionInfo.Name}");
                GameLogger.Log($"Player Counts : {_runner.SessionInfo.PlayerCount}");
            }
            else
            {
                    GameLogger.Log($"[Fusion] Failed to start {result.ShutdownReason}.");
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            NetworkInputData data = new NetworkInputData
            {
                Move = new Vector2(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"))
            };

            input.Set(data);
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
            
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
            
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ReadOnlySpan<byte> data)
        {
            
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
            
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
            
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
            
        }
    }
}