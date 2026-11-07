using Fusion;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Events;
using HelicopterTag.Gameplay.Managers;
using HelicopterTag.Gameplay.States;

namespace HelicopterTag.Networking.Fusion
{
    public class NetworkMatchController : NetworkBehaviour, IMatchPhaseAuthority
    {
        [Networked, OnChangedRender(nameof(OnPhaseChanged))]
        private MatchPhase CurrentPhase { get; set; }

        public GameManager _gameManager;

        public override void Spawned()
        {
            _gameManager = FindFirstObjectByType<GameManager>();

            LocalPlayerRegistry.RegisterMatchPhaseAuthority(this);

            ApplyPhase(CurrentPhase);
        }

        public override void Despawned(
            NetworkRunner runner, bool hasState)
        {
            LocalPlayerRegistry.UnregisterMatchPhaseAuthority(this);
        }

        public void RequestAdvance()
        {
            if (!HasStateAuthority)
                return;

            MatchPhase nextPhase = CurrentPhase switch
            {
                MatchPhase.Waiting => MatchPhase.Countdown,
                MatchPhase.Countdown => MatchPhase.Playing,
                MatchPhase.Playing => MatchPhase.Finished,
                _ => CurrentPhase
            };

            if (nextPhase == CurrentPhase)
                return;

            CurrentPhase = nextPhase;

            ApplyPhase(CurrentPhase);
        }

        private void OnPhaseChanged()
        {
            ApplyPhase(CurrentPhase);
        }

        private void ApplyPhase(MatchPhase phase)
        {
            if (_gameManager == null)
                return;

            _gameManager.ApplyNetworkPhase(phase);
        }
    }
}