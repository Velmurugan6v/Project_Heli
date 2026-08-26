using System;
using Fusion;
using HelicopterTag.Core;
using HelicopterTag.Core.Input;
using HelicopterTag.Gameplay.Camera;
using HelicopterTag.Gameplay.Player;
using HelicopterTag.Helicopter;
using UnityEngine;

namespace HelicopterTag.Networking.Fusion.Player
{
    public class NetworkPlayer : NetworkBehaviour
    {
        private InputData _localInput;

        [SerializeField] private HelicopterMotor _helicopterMotor;
        [SerializeField] private NetworkPlayerState _playerState;
        [SerializeField] private PlayerContext _playerContext;
        [SerializeField] private float _moveSpeed = 5f;

        public PlayerContext PlayerContext => _playerContext;
        public NetworkPlayerState PlayerState => _playerState;

        [Networked] private Vector2 NetworkMoveInput { get; set; }
        [Networked] private float NetworkLiftInput { get; set; }

        public override void Spawned()
        {
            GameLogger.Log($"[NetworkPlayer] " + $"Object : {name}" +
                           $"InputAuthority : {Object.InputAuthority}" + $"`IsMine : {HasInputAuthority}");

            if (_playerContext == null)
            {
                GameLogger.Log($"[NetworkPlayer] PlayerContext is missing on {name}");
                return;
            }

            if (!HasInputAuthority)
                return;

            HelicopterCamera heliCamera = FindFirstObjectByType<HelicopterCamera>();

            if (heliCamera == null)
            {
                GameLogger.Log($"[NetworkPlayer] No HelicopterCamera found on {name}");
                return;
            }

            heliCamera.SetTarget(transform);

            GameLogger.Log($"[NetworkPlayer] PlayerContext = {_playerContext.name}");
        }

        private void FixedUpdate()
        {
            if (Object == null)
                return;

            if (!HasInputAuthority)
            {
                _helicopterMotor.SetInput(new InputData
                {
                    Move = NetworkMoveInput,
                    Lift = NetworkLiftInput
                });
            }
        }

        //Fixed Update Network
        public override void FixedUpdateNetwork()
        {
            if (GetInput<NetworkInputData>(out NetworkInputData inputData))
            {
                if (Object.HasStateAuthority)
                {
                    NetworkMoveInput = inputData.Move;
                    NetworkLiftInput = inputData.Lift;
                }

                if (HasStateAuthority)
                {
                    _helicopterMotor.SetInput(new InputData()
                    {
                        Move = inputData.Move,
                        Lift = inputData.Lift
                    });
                }
            }
        }
    }
}