using Fusion;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Tag;
using HelicopterTag.Gameplay.Tag.Events;
using UnityEngine;

namespace HelicopterTag.Networking.Fusion.Player
{
    public class NetworkPlayerState : NetworkBehaviour
    {
        [SerializeField] private TagParticipant _tagParticipant;
        [Networked] public NetworkBool IsIt { get; private set; }
        [Networked] public int TagScore { get; private set; }
        [Networked] public float SurvivalTime { get; private set; }

        private bool _lastIsIt;

        private void OnEnable()
        {
            EventBus.Subscribe<ItChangedEvent>(OnItChanged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<ItChangedEvent>(OnItChanged);
        }

        private void OnItChanged(ItChangedEvent eventData)
        {
            if(!HasStateAuthority)
                return;

            if (eventData.PreviousIt == _tagParticipant)
            {
                SetIsIt(false);
            }
            
            if(eventData.CurrentIt==_tagParticipant)
                SetIsIt(false);
        }


        public override void Spawned()
        {
            SyncNetworkState();
            GameLogger.Log($"[NetworkPlayerState] Spawned : {name} | IsIt : {IsIt}");
        }
        

        public override void Render()
        {
            SyncNetworkState();
        }

        private void SyncNetworkState()
        {
            if (_tagParticipant == null)
                return;

            if (_lastIsIt == IsIt)
                return;

            _lastIsIt = IsIt;

            _tagParticipant.ApplyNetworkItState(IsIt);
        }


        public void SetIsIt(bool value)
        {
            if (!HasStateAuthority)
                return;

            IsIt = value;
        }

        public void AddTagScore(int amount)
        {
            if (!HasStateAuthority)
                return;

            TagScore += amount;
        }

        public void AddSurvivalTime(float amount)
        {
            if (!HasStateAuthority)
                return;

            SurvivalTime += amount;
        }

        public void ResetState()
        {
            if (!HasStateAuthority)
                return;

            IsIt = false;
            TagScore = 0;
            SurvivalTime = 0f;
        }
    }
}