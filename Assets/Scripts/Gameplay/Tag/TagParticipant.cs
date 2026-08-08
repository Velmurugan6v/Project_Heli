using System;
using HelicopterTag.Core;
using HelicopterTag.Gameplay.Player;
using UnityEngine;

namespace HelicopterTag.Gameplay.Tag
{
    public class TagParticipant : MonoBehaviour
    {
        private PlayerManager _playerManager;
        [SerializeField] private PlayerContext _playerContext;
        public PlayerContext PlayerContext => _playerContext;
        public bool IsIt { get; private set; }


        private void Awake()
        {
            _playerManager = FindFirstObjectByType<PlayerManager>();
            _playerManager.RegisterPlayer(_playerContext);
        }

        private void OnDestroy()
        {
            if (_playerManager != null)
            {
                _playerManager.UnregisterPlayer(_playerContext);
            }
        }

        public void SetAsIt()
        {
            IsIt = true;
            GameLogger.Log($"Tag Participant {gameObject.name} is set to {IsIt}");
        }

        public void RemoveIt()
        {
            IsIt = false;
            GameLogger.Log($"Tag Participant {gameObject.name} is set to {IsIt}");
        }
    }
}