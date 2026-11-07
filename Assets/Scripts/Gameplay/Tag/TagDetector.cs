using System;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using UnityEngine;

namespace HelicopterTag.Gameplay.Tag
{
    public class TagDetector : MonoBehaviour, ITagAction
    {
        [SerializeField] private TagParticipant _participant;
        [SerializeField] private TagManager _tagManager;

        [SerializeField] private float _tagRange = 10f;
        [SerializeField] private LayerMask _playerLayer;

        public bool CanTag => _participant != null && _participant.IsIt;

        private void Start()
        {
            _tagManager = FindFirstObjectByType<TagManager>();
        }

        public void TryTag()
        {
            if (!CanTag)
                return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, _tagRange, _playerLayer);

            foreach (Collider collider in colliders)
            {
                TagParticipant otherParticipant = collider.GetComponent<TagParticipant>();

                if (otherParticipant == null)
                    continue;

                if (otherParticipant == _participant)
                    continue;

                _tagManager.TransferIt(otherParticipant);

                GameLogger.Log($"Tag detected: {otherParticipant.name}");
                return;
            }

            GameLogger.Log($"No Helicopter in tag range");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _tagRange);
        }
    }
}