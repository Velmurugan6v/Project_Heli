using System.Collections.Generic;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Config;
using HelicopterTag.Gameplay.Events;
using HelicopterTag.Gameplay.Player;
using HelicopterTag.Gameplay.Tag.Events;
using UnityEngine;

namespace HelicopterTag.Gameplay.Tag
{
    public class TagManager : MonoBehaviour
    {
        [SerializeField] private List<TagParticipant> _participants;
        public IReadOnlyList<TagParticipant> Participants => _participants;

        private TagParticipant _currentIt;

        public TagParticipant CurrentIt => _currentIt;

        private TagParticipant _protectedParticipant;

        private float _protectionTimeRemaining;

        [SerializeField] private GameplayConfig _config;

        private bool _isTaggingActive;

        private void OnEnable()
        {
            EventBus.Subscribe<MatchStartedEvent>(OnMatchStarted);
            EventBus.Subscribe<MatchFinishedEvent>(OnMatchFinished);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MatchStartedEvent>(OnMatchStarted);
            EventBus.Unsubscribe<MatchFinishedEvent>(OnMatchFinished);
        }


        private void OnMatchStarted(MatchStartedEvent eventData)
        {
            _isTaggingActive = true;
            SelectInitialIt();
        }

        private void OnMatchFinished(MatchFinishedEvent eventData)
        {
            _isTaggingActive = false;
            _protectedParticipant = null;
            _protectionTimeRemaining = 0f;
        }

        private void Update()
        {
            UpdateTagProtection();
        }

        private void SelectInitialIt()
        {
            int participantCount = _participants.Count;
            int randomIndex = UnityEngine.Random.Range(0, participantCount);
            _currentIt = _participants[randomIndex];
            _currentIt.SetAsIt();

            EventBus.Publish(new ItChangedEvent(null, _currentIt));
        }

        private void RegisterParticipant(TagParticipant participant)
        {
        }

        private void UnregisterParticipant(TagParticipant participant)
        {
        }

        public void TransferIt(TagParticipant newIt)
        {
            if (!_isTaggingActive) return;
            if (newIt == null) return;
            if (newIt == _currentIt) return;
            if (newIt == _protectedParticipant) return;

            TagParticipant previousIt = _currentIt;
            previousIt?.RemoveIt();

            _currentIt = newIt;
            _currentIt.SetAsIt();

            _protectedParticipant = previousIt;
            _protectionTimeRemaining = _config.TagProtectionDuration;

            EventBus.Publish(new ItChangedEvent(previousIt, _currentIt));

            if (_protectedParticipant != null)
            {
                GameLogger.Log(_protectedParticipant.name + " is protecting.");
                EventBus.Publish(new TagProtectionChangedEvent(_protectedParticipant, true));
            }
        }

        private void UpdateTagProtection()
        {
            if (_protectedParticipant == null)
                return;

            _protectionTimeRemaining -= Time.deltaTime;

            if (_protectionTimeRemaining > 0f)
                return;

            TagParticipant participant = _protectedParticipant;

            _protectionTimeRemaining = 0;
            _protectedParticipant = null;

            EventBus.Publish(new TagProtectionChangedEvent(participant, false));
        }

        //Temp
        public IReadOnlyList<PlayerContext> GetPLayers()
        {
            List<PlayerContext> players = new();

            foreach (TagParticipant participant in _participants)
            {
                players.Add(participant.PlayerContext);
            }

            return players;
        }
    }
}