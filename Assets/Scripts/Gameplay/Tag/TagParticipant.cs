using HelicopterTag.Core;
using HelicopterTag.Gameplay.Player;
using UnityEngine;

namespace HelicopterTag.Gameplay.Tag
{
    public class TagParticipant : MonoBehaviour
    {
        [SerializeField] private PlayerContext _playerContext;
        public PlayerContext PlayerContext => _playerContext;
        public bool IsIt { get; private set; }


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