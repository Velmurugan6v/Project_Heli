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


        private void Start()
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
            _playerContext.MatchData.SetIt(true);
            GameLogger.Log($"Tag Participant {gameObject.name} is set to {IsIt}");
        }

        public void RemoveIt()
        {
            IsIt = false;
            _playerContext.MatchData.SetIt(false);
            GameLogger.Log($"Tag Participant {gameObject.name} is set to {IsIt}");
        }

        public void ApplyNetworkItState(bool value)
        {
            IsIt = value;
            _playerContext.MatchData.SetIt(value);
        }
    }
}