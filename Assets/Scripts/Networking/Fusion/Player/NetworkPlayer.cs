using Fusion;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Core.Input;
using HelicopterTag.Gameplay.Camera;
using HelicopterTag.Gameplay.Player;
using HelicopterTag.Gameplay.Tag;
using HelicopterTag.Helicopter;
using HelicopterTag.UI.Gameplay;
using UnityEngine;

namespace HelicopterTag.Networking.Fusion.Player
{
    public class NetworkPlayer : NetworkBehaviour
    {
        private InputData _localInput;
        private bool _tagRequested;

        [SerializeField] private HelicopterMotor _helicopterMotor;
        [SerializeField] private NetworkPlayerState _playerState;
        [SerializeField] private PlayerContext _playerContext;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private TagDetector _tagDetector;
        [SerializeField] private TagButtonUI _tagButtonUI;

        public PlayerContext PlayerContext => _playerContext;
        public NetworkPlayerState PlayerState => _playerState;

        [Networked] private Vector2 NetworkMoveInput { get; set; }
        [Networked] private float NetworkLiftInput { get; set; }
        
        private TagManager _tagManager;
        private TagParticipant _tagParticipant;

        public override void Spawned()
        {
            GameLogger.Log($"[NetworkPlayer] Object : {name}" +
                           $"InputAuthority : {Object.InputAuthority}" + $"`IsMine : {HasInputAuthority}");

            if (_playerContext == null)
            {
                GameLogger.Log($"[NetworkPlayer] PlayerContext is missing on {name}");
                return;
            }

            if (!HasInputAuthority)
                return;

            if (Object.HasStateAuthority)
            {
                _tagManager = FindFirstObjectByType<TagManager>();
                _tagParticipant = GetComponent<TagParticipant>();

                if (_tagManager != null && _tagParticipant != null)
                {
                    _tagManager.RegisterParticipant(_tagParticipant);
                }
            }

            HelicopterCamera heliCamera = FindFirstObjectByType<HelicopterCamera>();

            if (heliCamera == null)
            {
                GameLogger.Log($"[NetworkPlayer] No HelicopterCamera found on {name}");
                return;
            }

            if (HasInputAuthority)
            {
                LocalPlayerRegistry.RegisterLocalTagAction(_tagDetector);
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
            if (!GetInput<NetworkInputData>(out NetworkInputData inputData))
                return;

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

                if (inputData.Tag)
                {
                    GameLogger.Log($"[NetworkPlayer] Tag REQUEST from {gameObject.name}");
                }
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if (Object.HasStateAuthority &&
                _tagManager != null &&
                _tagParticipant != null)
            {
                _tagManager.UnregisterParticipant(_tagParticipant);
            }
        }


        public void Requested()
        {
            _tagRequested = true;
        }
    }
}